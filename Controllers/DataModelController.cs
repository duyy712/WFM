using Breeze.WebApi2;
using Breeze.ContextProvider.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WFM.Models;
using Breeze.ContextProvider;
using Newtonsoft.Json.Linq;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace WFM.Controllers
{
    [BreezeController]
   // [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [EnableBreezeQuery]
    public class DataModelController : ApiController
    {
        private readonly EFContextProvider<DataModelContext> _contextProvider = new EFContextProvider<DataModelContext>();

        // ~/breeze/datamodel/Metadata 
        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        [HttpGet]
        public IQueryable<Customer> Customers()
        {
            return _contextProvider.Context.Customers;
        }

        [HttpGet]
        public IQueryable<CustomerRequest> CustomerRequest()
        {
            return _contextProvider.Context.CustomerRequests;
        }

        [HttpGet]
        public IQueryable<Employee> Employees()
        {
            return _contextProvider.Context.Employees;
        }

        [HttpGet]
        public IQueryable<Topic> Topics()
        {
            return _contextProvider.Context.Topics;
        }

        [HttpGet]
        public IQueryable<SourceFile> SourceFiles()
        {
            return _contextProvider.Context.SourceFiles;
        }


        public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            public CustomMultipartFormDataStreamProvider(string path) : base(path)
            {
            }

            public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
            {
                var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
                return name.Replace("\"", string.Empty);
            }
        }

        public Task<IEnumerable<SourceFile>> Import()
        {
            var folderName = "uploads";
            var PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
            var rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);
                var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IEnumerable<SourceFile>>(t =>
                {

                    if (t.IsFaulted || t.IsCanceled)
                    {
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                    }

                    var fileInfo = streamProvider.FileData.Select(i => {
                        var info = new FileInfo(i.LocalFileName);
                        return new SourceFile(info.Name, rootUrl + "/" + folderName + "/" + info.Name);
                    });
                    return fileInfo;
                });

                return task;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }
        protected void AfterSaveEntities(Dictionary<Type, List<EntityInfo>> saveMap, List<KeyMapping> keyMappings)
        {
            foreach (var item in saveMap)
            {
                foreach (var entityItem in item.Value)
                {
                    if (entityItem.Entity is Customer && (entityItem.EntityState == EntityState.Added))
                    {

                        var entity = (Customer)entityItem.Entity;
                        //var maxCode= _contextProvider.Context.Tables.Max(x => Convert.ToInt32(x.Code));
                        //entity.Code = (maxCode + 1).ToString();
                        entity.Code = "KH" + entity.Id.ToString();
                        // _contextProvider.Context.Entry(entity).Property(u => u.Code).CurrentValue = entity.ID.ToString();
                        entityItem.OriginalValuesMap["Code"] = null;
                    }
                    if (entityItem.Entity is CustomerRequest && (entityItem.EntityState == EntityState.Added))
                    {
                        var entity = (CustomerRequest)entityItem.Entity;
                        entity.Name = "REQ " + entity.Id;
                        entity.DateCreated = DateTime.Now;
                        entityItem.OriginalValuesMap["Code"] = null;
                        entityItem.OriginalValuesMap["DateCreated"] = null;
                    }

                }


            }
            _contextProvider.Context.SaveChanges();

        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            //  _contextProvider.BeforeSaveEntityDelegate += BeforeSaveEntity;
            _contextProvider.AfterSaveEntitiesDelegate += AfterSaveEntities;
            return _contextProvider.SaveChanges(saveBundle);
        }


    }
}
