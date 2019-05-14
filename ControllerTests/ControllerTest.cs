using System;
using System.Web;
using NUnit.Framework;
using Entities.Models;
using OpportunityManagement.Controllers;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;
using LoggerService;
using Repository;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace ControllerTests
{
    [TestFixture]
    public class ControllerTest
    {

        ILoggerManager _logger;
        IRepositoryWrapper _repository;
        [Test]
        public void TestUserIndex()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestUserIndex").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new UserController(_logger, _repository);
            var actResult = obj.Index() as ViewResult;
            Assert.That(actResult.ViewName, Is.EqualTo("Login"));
        }

        [Test]
        public void TestUserLogin()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestUserLogin").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new UserController(_logger, _repository);
            var actResult = obj.Login() as ViewResult;
            Assert.AreEqual("Login", actResult.ViewName);
        }

        [Test]
        public void TestUserLoginPost()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestUserLoginPost").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new UserController(_logger, _repository);
            seed(_context);
            var actResult1 = obj.Login(new Login() { UserName = "admin", Password = "admin" }) as ViewResult;
            var actResult2 = obj.Login(new Login() { UserName = "doctor1", Password = "doctor1" }) as ViewResult;
            var actResult3 = obj.Login(new Login() {UserName = "nurse1", Password = "nurse1"}) as ViewResult;
            var actResult4 = obj.Login(new Login() { UserName = "nurse9", Password = "nurse9" }) as NotFoundResult;
            //Assert.AreEqual("Index", actResult2.ViewName);
            //Assert.AreEqual("Index", actResult3.ViewName);
            //Assert.That(actResult.ViewName, Is.EqualTo("NavBar"));
            //var actResult = obj.Login() as ViewResult;
            //Assert.AreEqual("Login", actResult.ViewName);
        }

        [Test]
        public void TestUserApply()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestUserApply").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new UserController(_logger, _repository);
            seed(_context);
            UserController._loggedInUser = "doctor1";
            var actResult1 = obj.Apply(3) as ViewResult;
            var actResult2 = obj.Apply(10) as ViewResult;
            //Assert.AreEqual("Index", actResult2.ViewName);
            //Assert.AreEqual("Index", actResult3.ViewName);
            //Assert.That(actResult.ViewName, Is.EqualTo("NavBar"));
            //var actResult = obj.Login() as ViewResult;
            //Assert.AreEqual("Login", actResult.ViewName);
        }

        [Test]
        public void TestAdmin()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdmin").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            var actResult = obj.Admin() as ViewResult;
            Assert.That(actResult.ViewName, Is.EqualTo("Admin"));
        }

        [Test]
        public void TestAdminCreate()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminCreate").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            var actResult = obj.Create() as ViewResult;
            Assert.That(actResult.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void TestAdminRequests()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminRequests").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            var actResult = obj.Requests() as ViewResult;
            Assert.That(actResult.ViewName, Is.EqualTo("Request"));
        }

        [Test]
        public void TestAdminGetEvents()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminGetEvents").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            seed(_context);
            var actResult = obj.GetEvents() as JsonResult;
            Assert.IsAssignableFrom<JsonResult>(actResult);
            //Assert.AreEqual(ev, actResult);
        }

        [Test]
        public void TestAdminCreateDate()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminCreateDate").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            seed(_context);
            var actResult1 = obj.CreateDate("2019-05-08") as JsonResult;
            var actResult2 = obj.CreateDate("2019-05-06") as JsonResult;
            Assert.IsAssignableFrom<JsonResult>(actResult1);
        }

        [Test]
        public void TestAdminCreates()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminCreates").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            Opportunity opportunity = new Opportunity { };
            opportunity.OpportunityDescription = "Test";
            opportunity.StartTime = new DateTime(2019, 05, 15);
            opportunity.EndTime = new DateTime(2019, 05, 16);
            opportunity.IsVacant = "true";
            opportunity.Color = "red";
            var actResult = obj.Create(opportunity) as RedirectToActionResult;
            Assert.That(actResult.ActionName, Is.EqualTo("opportunities"));
        }

        [Test]
        public void TestAdminCreatesIfModelInvalid()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminCreatesIfModelStateInvalid").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            seed(_context);
            var opportunity = new Opportunity();
            var context = new ValidationContext(opportunity, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(opportunity, context, results, true);
            obj.ModelState.AddModelError("test", "test");
            var result = obj.Create(opportunity);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Test]
        public void TestAdminCreatesIfNull()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminCreatesIfModelStateInvalid").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            Opportunity opportunity = new Opportunity { };
            seed(_context);
            var result = obj.Create(opportunity);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Test]
        public void TestAdminUpdate()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminUpdate").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            seed(_context);
            var actResult1 = obj.Update(3) as ViewResult;
            var actResult2 = obj.Update(30) as ViewResult;
            Assert.That(actResult1.ViewName, Is.EqualTo("Update"));
        }

        [Test]
        public void TestAdminUpdates()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminUpdates").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            Opportunity opportunity = new Opportunity { };
            opportunity.OpportunityDescription = "Test";
            opportunity.StartTime = new DateTime(2019, 05, 15);
            opportunity.EndTime = new DateTime(2019, 05, 16);
            opportunity.IsVacant = "true";
            opportunity.Color = "red";
            seed(_context);
            var actResult = obj.Update(2, opportunity) as RedirectToActionResult;
            Assert.That(actResult.ActionName, Is.EqualTo("opportunities"));
        }

        [Test]
        public void TestAdminUpdatesIfModelInvalid()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminUpdatesIfModelInvalid").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            seed(_context);
            var opportunity = new Opportunity();
            var context = new ValidationContext(opportunity, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(opportunity, context, results, true);
            obj.ModelState.AddModelError("test", "test");
            var result = obj.Update(2, opportunity);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Test]
        public void TestAdminUpdatesIfNotFound()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminUpdatesIfNotFound").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            Opportunity opportunity = new Opportunity { };
            opportunity.OpportunityDescription = "Test";
            opportunity.StartTime = new DateTime(2019, 05, 15);
            opportunity.EndTime = new DateTime(2019, 05, 16);
            opportunity.IsVacant = "true";
            opportunity.Color = "red";
            seed(_context);
            var result = obj.Update(20, opportunity);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Test]
        public void TestAdminUpdateRequestStatus()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminUpdateRequestStatus").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            seed(_context);
            var actResult1 = obj.UpdateRequestStatus(2) as RedirectToActionResult;
            var actResult2 = obj.UpdateRequestStatus(20) as RedirectToActionResult;
            Assert.That(actResult1.ActionName, Is.EqualTo("opportunities"));
        }

        [Test]
        public void TestAdminDelete()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "TestAdminDelete").Options;
            var _context = new RepositoryContext(options);
            _logger = new LoggerManager();
            _repository = new RepositoryWrapper(_context);
            var obj = new AdminController(_logger, _repository);
            seed(_context);
            var actResult1 = obj.Delete(2) as RedirectToActionResult;
            var actResult2 = obj.Delete(20) as RedirectToActionResult;
            Assert.That(actResult1.ActionName, Is.EqualTo("opportunities"));
        }

        private void seed(RepositoryContext context)                        //seeding data to our in memory database
        {
            var users = new[]
            {
                new User{ user_id=1, UserName="admin", Password="ISMvKXpXpadDiUoOSoAfww==", Experience=1, Qualification="test", Role="Admin" },
                new User{ user_id=2, UserName="doctor1", Password="RfZ4sUf98nXDW2C6wjYJhA==", Experience=2, Qualification="test", Role="Doctor" },
                new User{user_id=3, UserName="nurse1", Password="r5/KrpEaq9CRdoHWkFVh/w==", Experience=3, Qualification="test", Role="Nurse" },
                

            };
            var opps = new[]
            {
                new Opportunity{o_id=1, OpportunityDescription="test", StartTime=new DateTime(), EndTime=new DateTime(), IsVacant="true", Color="red"},
                new Opportunity{o_id=2, OpportunityDescription="test", StartTime=new DateTime(), EndTime=new DateTime(), IsVacant="true", Color="red"},
                new Opportunity{o_id=3, OpportunityDescription="test", StartTime=new DateTime(), EndTime=new DateTime(), IsVacant="true", Color="red"},
                new Opportunity{o_id=4, OpportunityDescription="test", StartTime=new DateTime(), EndTime=new DateTime(), IsVacant="true", Color="red"},
                new Opportunity{o_id=5, OpportunityDescription="test", StartTime=new DateTime(), EndTime=new DateTime(), IsVacant="true", Color="red"}
            };
            var user_opps = new[]
           {
                new User_Opportunity{user_opportunity_id=1, Opportunity_Id=1, User_Id=1, Request_Date=new DateTime(), Is_Accepted="false"},
                new User_Opportunity{user_opportunity_id=2, Opportunity_Id=2, User_Id=2, Request_Date=new DateTime(), Is_Accepted="false"},
                new User_Opportunity{user_opportunity_id=3, Opportunity_Id=3, User_Id=3, Request_Date=new DateTime(), Is_Accepted="false"},
                
            };
            context.Users.AddRange(users);
            context.Opportunities.AddRange(opps);
            context.User_Opportunities.AddRange(user_opps);
            context.SaveChangesAsync();
        }
    }
}
