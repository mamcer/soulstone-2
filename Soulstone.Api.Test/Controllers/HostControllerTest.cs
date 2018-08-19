using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrossCutting.Core.Logging;
using CrossCutting.MainModule.IOC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Soulstone.Api.Controllers;
using Soulstone.Data;
using Soulstone.Entities;

namespace Soulstone.Api.Test.Controllers
{
    [TestClass]
    public class HostControllerTest
    {
        //private HttpRequestMessage _request;
        //private HostController _hostController;

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    //Mock Log Functionality
        //    var mockLogWriter = new Mock<ILogWriter>();
        //    mockLogWriter.Setup(l => l.Write(It.IsAny<string>()));
        //    var mockApplicationLogger = new Mock<IApplicationLogger>();
        //    mockApplicationLogger.Setup(m => m.Error).Returns(mockLogWriter.Object);
        //    var mockLogManager = new Mock<ILogManager>();
        //    mockLogManager.Setup(l => l.DefaultLogger).Returns(mockApplicationLogger.Object);
        //    IocUnityContainer.Instance.RegisterInstance(typeof (ILogManager), mockLogManager.Object);
            
        //    //Fake request information
        //    string uri = "http://localhost/api/";
        //    _request = new HttpRequestMessage(HttpMethod.Get, uri)
        //        {
        //            RequestUri = new Uri(uri)
        //        };
        //    _request.SetConfiguration(new HttpConfiguration(new HttpRouteCollection("")));
        //    _hostController = new HostController
        //    {
        //        Request = _request
        //    };
        //}

        //[TestMethod]
        //public void GetWithoutParameterShouldReturnAllHosts()
        //{
        //    //Arrange
        //    var mockRepository = new Mock<IRepository<>>();
        //    var name = "Host 1";
        //    mockRepository.Setup(r => r.GetAll()).Returns(new List<Host>
        //        {
        //            new Host
        //                {
        //                    Id = 1,
        //                    Name = name
        //                },
        //            new Host
        //                {
        //                    Id = 1,
        //                    Name = "Host 2"
        //                }
        //        });
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);
        //    List<HostDto> result;

        //    //Act
        //    result = _hostController.Get().ToList();

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(2, result.Count);
        //    Assert.AreEqual(name, result[0].Name);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(HttpResponseException))]
        //public void GetOnExceptionShouldReturnHttpResponseException()
        //{
        //    //Arrange
        //    var mockRepository = new Mock<IHostRepository>();
        //    mockRepository.Setup(r => r.GetAll()).Throws(new Exception("Exception Message"));
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);

        //    //Act
        //    _hostController.Get();
        //}

        //[TestMethod]
        //public void GetWithParameterShouldReturnSpecificHost()
        //{
        //    //Arrange
        //    var mockRepository = new Mock<IHostRepository>();
        //    var name = "Host 1";
        //    mockRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(new Host
        //        {
        //            Id = 1,
        //            Name = name
        //        });
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);
        //    HttpResponseMessage result;

        //    //Act
        //    result = _hostController.Get(1);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        //    Assert.AreEqual(name, ((HostDto)((ObjectContent)result.Content).Value).Name);
        //}

        //[TestMethod]
        //public void GetWithParameterAndNullHostShouldReturnErrorResponse()
        //{
        //    //Arrange
        //    var mockRepository = new Mock<IHostRepository>();
        //    mockRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(() => null);
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);
        //    HttpResponseMessage result;

        //    //Act
        //    result = _hostController.Get(1);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(HttpResponseException))]
        //public void GetWithParameterOnExceptionShouldReturnHttpResponseException()
        //{
        //    //Arrange
        //    var mockRepository = new Mock<IHostRepository>();
        //    mockRepository.Setup(r => r.Get(It.IsAny<int>())).Throws<Exception>();
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);

        //    //Act
        //    _hostController.Get(1);
        //}

        //[TestMethod]
        //public void PostShouldSaveHost()
        //{
        //    //Arrange
        //    int id = 99;
        //    var mockRepository = new Mock<IHostRepository>();
        //    mockRepository.Setup(r => r.Save(It.IsAny<Host>())).Returns((Host host) =>
        //        {
        //            host.Id = id;
        //            return host;
        //        });
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);
        //    HttpResponseMessage result;
        //    HostDto hostDto = new HostDto
        //        {
        //            Name = "New Host"
        //        };

        //    //Act
        //    result = _hostController.Post(hostDto);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        //    Assert.AreEqual(id, ((HostDto)((ObjectContent)result.Content).Value).Id);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(HttpResponseException))]
        //public void PostOnExceptionShouldReturnHttpResponseException()
        //{
        //    //Arrange
        //    var mockRepository = new Mock<IHostRepository>();
        //    mockRepository.Setup(r => r.Save(It.IsAny<Host>())).Throws<Exception>();
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);

        //    //Act
        //    _hostController.Post(new HostDto());
        //}

        //[TestMethod]
        //public void PutShouldUpdateHost()
        //{
        //    //Arrange
        //    int id = 1;
        //    var mockRepository = new Mock<IHostRepository>();
        //    mockRepository.Setup(m => m.Get(It.IsAny<int>())).Returns(new Host());
        //    mockRepository.Setup(r => r.Update(It.IsAny<Host>())).Returns((Host host) =>
        //    {
        //        return host;
        //    });
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);
        //    HttpResponseMessage result;
        //    HostDto hostDto = new HostDto
        //    {
        //        Name = "New Host"
        //    };

        //    //Act
        //    result = _hostController.Put(id, hostDto);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        //}

        //[TestMethod]
        //public void PutWithNonExistingHostShouldReturnBadRequest()
        //{
        //    //Arrange
        //    int id = 1;
        //    var mockRepository = new Mock<IHostRepository>();
        //    mockRepository.Setup(m => m.Get(It.IsAny<int>())).Returns(() => null);
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);
        //    HttpResponseMessage result;
        //    HostDto hostDto = new HostDto
        //    {
        //        Name = "New Host"
        //    };

        //    //Act
        //    result = _hostController.Put(id, hostDto);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(HttpResponseException))]
        //public void PutOnExceptionShouldReturnHttpResponseException()
        //{
        //    //Arrange
        //    int id = 1;
        //    var mockRepository = new Mock<IHostRepository>();
        //    mockRepository.Setup(m => m.Get(It.IsAny<int>())).Returns(new Host());
        //    mockRepository.Setup(r => r.Update(It.IsAny<Host>())).Throws<Exception>();
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);

        //    //Act
        //    _hostController.Put(id, new HostDto());
        //}

        //[TestMethod]
        //public void DeleteShouldDeleteHost()
        //{
        //    //Arrange
        //    int id = 1;
        //    var mockRepository = new Mock<IHostRepository>();
        //    mockRepository.Setup(m => m.Get(It.IsAny<int>())).Returns(new Host());
        //    mockRepository.Setup(r => r.Delete(It.IsAny<Host>()));
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);
        //    HttpResponseMessage result;

        //    //Act
        //    result = _hostController.Delete(id);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        //}

        //[TestMethod]
        //public void DeleteWithNonExistingHostShouldReturnBadRequest()
        //{
        //    //Arrange
        //    int id = 1;
        //    var mockRepository = new Mock<IHostRepository>();
        //    mockRepository.Setup(m => m.Get(It.IsAny<int>())).Returns(() => null);
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);
        //    HttpResponseMessage result;
        
        //    //Act
        //    result = _hostController.Delete(id);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(HttpResponseException))]
        //public void DeleteOnExceptionShouldReturnHttpResponseException()
        //{
        //    //Arrange
        //    int id = 1;
        //    var mockRepository = new Mock<IHostRepository>();
        //    mockRepository.Setup(m => m.Get(It.IsAny<int>())).Returns(new Host());
        //    mockRepository.Setup(r => r.Delete(It.IsAny<Host>())).Throws<Exception>();
        //    IocUnityContainer.Instance.RegisterInstance(typeof(IHostRepository), mockRepository.Object);

        //    //Act
        //    _hostController.Delete(id);
        //}
    }
}