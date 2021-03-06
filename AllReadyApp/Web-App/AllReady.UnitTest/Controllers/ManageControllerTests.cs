﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AllReady.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MediatR;
using AllReady.ViewModels.Manage;
using static AllReady.Controllers.ManageController;
using AllReady.Models;
using AllReady.UnitTest.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using AllReady.Features.Manage;
using AllReady.Features.Login;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AllReady.UnitTest.Controllers
{
    public class ManageControllerTests
    {
        //delete this line when all unit tests using it have been completed
        private static readonly Task TaskCompletedTask = Task.CompletedTask;

        [Fact]
        public async Task IndexGetAddsCorrectMessageToViewDataWhenMessageEqualsChangePasswordSuccess()
        {           
            //Mock controller dependencies UserManager, signinmanager and IMediator, set behaviour of called methods
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser());

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userId");
          
            var result = await controller.Index(ManageMessageId.ChangePasswordSuccess);
            var resultViewModel = ((ViewResult)result);
            var message = resultViewModel.ViewData["StatusMessage"].ToString();
         
            Assert.Equal("Your password has been changed.", message);
        }

        [Fact]
        public async Task IndexGetAddsCorrectMessageToViewDataWhenMessageIdEqualsSetPasswordSuccess()
        {          
            //Mock controller dependencies UserManager, signinmanager and IMediator, set behaviour of called methods
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser());

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userId");
            
            var result = await controller.Index(ManageMessageId.SetPasswordSuccess);
            var resultViewModel = ((ViewResult)result);
            var message = resultViewModel.ViewData["StatusMessage"].ToString();
           
            Assert.Equal("Your password has been set.", message);
        }

        [Fact]
        public async Task IndexGetAddsCorrectMessageToViewDataWhenMessageIdEqualsSetTwoFactorSuccess()
        {
            
            //Mock controller dependencies UserManager, signinmanager and IMediator, set behaviour of called methods
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser());

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userId");
            
            var result = await controller.Index(ManageMessageId.SetTwoFactorSuccess);
            var resultViewModel = ((ViewResult)result);
            var message = resultViewModel.ViewData["StatusMessage"].ToString();
          
            Assert.Equal("Your two-factor authentication provider has been set.", message);
        }

        [Fact]
        public async Task IndexGetAddsCorrectMessageToViewDataWhenMessageIdEqualsError()
        {        
            //Mock controller dependencies UserManager, signinmanager and IMediator, set behaviour of called methods
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser());

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userId");
           
            var result = await controller.Index(ManageMessageId.Error);
            var resultViewModel = ((ViewResult)result);
            var message = resultViewModel.ViewData["StatusMessage"].ToString();
           
            Assert.Equal("An error has occurred.", message);
        }

        [Fact]
        public async Task IndexGetAddsCorrectMessageToViewDataWhenMessageIdEqualsAddPhoneSuccess()
        {          
            //Mock controller dependencies UserManager, signinmanager and IMediator, set behaviour of called methods
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser());

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userId");
           
            var result = await controller.Index(ManageMessageId.AddPhoneSuccess);
            var resultViewModel = ((ViewResult)result);
            var message = resultViewModel.ViewData["StatusMessage"].ToString();

            
            Assert.Equal("Your mobile phone number was added.", message);
        }

        [Fact]
        public async Task IndexGetAddsCorrectMessageToViewDataWhenMessageIdEqualsRemovePhoneSuccess()
        {          
            //Mock controller dependencies UserManager, signinmanager and IMediator, set behaviour of called methods
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser());

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userId");
          
            var result = await controller.Index(ManageMessageId.RemovePhoneSuccess);
            var resultViewModel = ((ViewResult)result);
            var message = resultViewModel.ViewData["StatusMessage"].ToString();
            
            Assert.Equal("Your mobile phone number was removed.", message);
        }

        [Fact]
        public async Task IndexGetSendsUserByUserIdQueryWithCorrectUserId()
        {           
            var userId = "userId";

            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.SendAsync(It.IsAny<AllReady.Features.Manage.UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser ());

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser(userId);

            await controller.Index();
            mediator.Verify(m => m.SendAsync(It.Is<AllReady.Features.Manage.UserByUserIdQuery>(u => u.UserId == userId)),Times.Once);
        }

        [Fact]
        public async Task IndexGetReturnsCorrectView()
        {           
            //Mock controller dependencies UserManager, signinmanager and IMediator, set behaviour of called methods
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser());

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userId");
            
            var result = await controller.Index();
            
            Assert.IsType(typeof(ViewResult),result);
        }

        [Fact]
        public void IndexGetHasHttpGetAttribute()
        {           
            var controller = new ManageController(null, null, null);
            
            var attribute = controller.GetAttributesOn(x => x.Index(It.IsAny<ManageMessageId>())).OfType<HttpGetAttribute>().SingleOrDefault();
            
            Assert.NotNull(attribute);
        }

        [Fact]
        public async Task IndexGetReturnsCorrectViewModel()
        {
            
            //Mock controller dependencies UserManager, signinmanager and IMediator, set behaviour of called methods
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser());

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userId");
           
            var result = await controller.Index(ManageMessageId.RemovePhoneSuccess);
            var resultViewModel = ((ViewResult)result);
            var vm = (IndexViewModel)resultViewModel.ViewData.Model;
           
            Assert.IsType<IndexViewModel>(vm);
        }

        [Fact]
        public async Task IndexPostSendsUserByUserIdQueryWithCorrectUserId()
        {           
            var userId = "userId";

            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.SendAsync(It.IsAny<AllReady.Features.Manage.UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser());

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser(userId);

            var vm = new IndexViewModel();

            await controller.Index(vm);
            mediator.Verify(m => m.SendAsync(It.Is<AllReady.Features.Manage.UserByUserIdQuery>(u => u.UserId == userId)), Times.Once);
        }

        [Fact]
        public async Task IndexPostReturnsCorrectViewWhenModelStateIsInvalid()
        {          
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser());
            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userId");
            IndexViewModel invalidVm = new IndexViewModel();
            controller.ModelState.AddModelError("FirstName", "Can't be a number");
            
            var result = await controller.Index(invalidVm);
          
            Assert.IsType(typeof(ViewResult), result);
        }

        [Fact]
        public async Task IndexPostReturnsCorrectViewModelWhenModelStateIsInvalid()
        {            
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser());
            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userId");
            IndexViewModel invalidVm = new IndexViewModel();
            controller.ModelState.AddModelError("FirstName", "Can't be a number");
           
            var result = await controller.Index(invalidVm);
            var resultViewModel = ((ViewResult)result);
            var vm = (IndexViewModel)resultViewModel.ViewData.Model;
            
            Assert.IsType<IndexViewModel>(vm);
        }

        [Fact]
        public async Task IndexPostInvokesRemoveClaimsAsyncWithCorrectParametersWhenUsersTimeZoneDoesNotEqualModelsTimeZone()
        {         
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(x => x.RemoveClaimsAsync(It.IsAny<ApplicationUser>(), It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(IdentityResult.Success);

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            var user = new ApplicationUser { TimeZoneId = "timeZoneId" };
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(user);

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userId");
            var vM = new IndexViewModel { TimeZoneId = "differentTimeZoneId" };
           
            await controller.Index(vM);
         
            IEnumerable<Claim> claims = controller.User.Claims.Where(c => c.Type == AllReady.Security.ClaimTypes.TimeZoneId).ToList();
            userManagerMock.Verify(x => x.RemoveClaimsAsync(user, claims), Times.Once);
        }

        [Fact]
        public async Task IndexPostInvokesAddClaimAsyncWithCorrectParametersWhenUsersTimeZoneDoesNotEqualModelsTimeZone()
        {            
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(x => x.AddClaimAsync(It.IsAny<ApplicationUser>(), It.IsAny<Claim>())).ReturnsAsync(IdentityResult.Success);

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);

            var mediator = new Mock<IMediator>();
            var user = new ApplicationUser { TimeZoneId = "timeZoneId" };
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(user);

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userId");
            var vM = new IndexViewModel { TimeZoneId = "differentTimeZoneId" };
            
            await controller.Index(vM);
           
            userManagerMock.Verify(x => x.AddClaimAsync(user, It.Is<Claim>(c=>c.Type == AllReady.Security.ClaimTypes.TimeZoneId)), Times.Once);
        }

        //TODO: come back to finsih these stubs... there is a lot going on in Index Post

        [Fact]
        public void IndexPostHasHttpPostAttrbiute()
        {           
            var controller = new ManageController(null, null, null);
            
            var attribute = controller.GetAttributesOn(x => x.Index(It.IsAny<IndexViewModel>())).OfType<HttpPostAttribute>().SingleOrDefault();
            
            Assert.NotNull(attribute);
        }

        [Fact]
        public void IndexPostHasValidateAntiForgeryTokenAttribute()
        {
            var controller = new ManageController(null, null, null);

            var attribute = controller.GetAttributesOn(x => x.Index(It.IsAny<IndexViewModel>())).OfType<ValidateAntiForgeryTokenAttribute>().SingleOrDefault();

            Assert.NotNull(attribute);
        }
        [Fact]
        public async Task IndexPostSendsRemoveUserProfileIncompleteClaimCommandWithCorrectUserIdWhenUsersProfileIsComplete()
        {
            ApplicationUser user = new ApplicationUser
            {
                Id = "Some UserID",
                Email = "email@company.com",
                FirstName = "Name",
                LastName = "Last Name",
                PhoneNumber = "01234567890",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                TimeZoneId = "TimeZonedID",
            };

            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);
            signInManagerMock.Setup(m => m.RefreshSignInAsync(It.IsAny<ApplicationUser>())).Returns(Task.FromResult(user));

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(user);

            var manageController = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            manageController.SetFakeUser(user.Id);
            
            var viewModel = new IndexViewModel { FirstName = "Name", LastName = "Last Name", TimeZoneId = "TimeZonedID"};

            await manageController.Index(viewModel);

            mediator.Verify(m => m.SendAsync(It.Is<RemoveUserProfileIncompleteClaimCommand>(u => u.UserId == user.Id)), Times.Once);
        }

        [Fact]
        public async Task UpdateUserProfileCompletenessInvokesRefreshSignInAsyncWithCorrectUserWhenUsersProfileIsComplete()
        {
            //Set properties of user required for ApplicationUser.IsProfileComplete() to return true
            ApplicationUser user = new ApplicationUser
            {
                Id = "Some UserID",
                Email = "email@company.com",
                FirstName = "Name",
                LastName = "Last Name",
                PhoneNumber = "01234567890",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                TimeZoneId = "TimeZonedID"
            };

            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);
            signInManagerMock.Setup(m => m.RefreshSignInAsync(It.IsAny<ApplicationUser>())).Returns(Task.FromResult(user));

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(user);

            var manageController = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            manageController.SetFakeUser(user.Id);

            //Only set props required for modelstate to be valid.
            IndexViewModel viewModel = new IndexViewModel { FirstName = "Name", LastName = "Last Name", TimeZoneId = "TimeZonedID" };

            await manageController.Index(viewModel);

            signInManagerMock.Verify(s=>s.RefreshSignInAsync(It.Is<ApplicationUser>(u=>u == user)),Times.AtLeastOnce);
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ResendEmailConfirmationInvokesGetUserAsyncWithCorrectUserId()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }
        [Fact]
        public async Task ResendEmailConfirmationInvokesGenerateEmailConfirmationTokenAsyncWithCorrectUser()
        {
            ApplicationUser user = new ApplicationUser { Id = "MyUserID" };
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            userManagerMock.Setup(u => u.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync("token");

            var mediator = new Mock<IMediator>();

            ManageController controller = new ManageController(userManagerMock.Object, null, mediator.Object);
            controller.SetFakeIUrlHelper();
            controller.SetFakeUser(user.Id);

            await controller.ResendEmailConfirmation();

            userManagerMock.Verify(u => u.GenerateEmailConfirmationTokenAsync(It.Is<ApplicationUser>(i => i == user)), Times.Once);
        }

        [Fact]
        public async Task ResendEmailConfirmationInvokesUrlActionWithCorrectParameters()
        {
            ApplicationUser user = new ApplicationUser { Id = "MyUserID" };
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            const string code = "token";
            userManagerMock.Setup(u => u.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(code);

            var controller = new ManageController(userManagerMock.Object, null, Mock.Of<IMediator>());
            controller.SetFakeUser(user.Id);
            var urlMock = new Mock<IUrlHelper>();
            controller.Url = urlMock.Object;
            urlMock.Setup(u => u.Action(It.IsAny<UrlActionContext>())).Returns("callbackUrl");

            controller.SetFakeHttpRequestSchemeTo("scheme");

            await controller.ResendEmailConfirmation();

            var urlcontext = new UrlActionContext
            {
                Action = nameof(controller.ConfirmNewEmail),
                Controller = "Account",
                Values = new { userId = user.Id, code }
            };

            urlMock.Verify(a => a.Action(It.Is<UrlActionContext>(i => i.Action == urlcontext.Action && i.Controller == urlcontext.Controller && i.Values.ToString() == $"{{ userId = {user.Id}, code = token }}" && i.Protocol == controller.HttpContext.Request
            .Scheme)),Times.Once);
        }

        [Fact]
        public async Task ResendEmailConfirmationSendsSendConfirmAccountEmailAsyncWithCorrectData()
        {
            const string callBackUrl = "callbackUrl";

            ApplicationUser user = new ApplicationUser { Id = "MyUserID",Email="me@email.com" };
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            userManagerMock.Setup(u => u.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync("");

            var mediator = new Mock<IMediator>();

            ManageController controller = new ManageController(userManagerMock.Object, null, mediator.Object);
            controller.SetFakeUser(user.Id);
            var urlMock = new Mock<IUrlHelper>();
            controller.Url = urlMock.Object;
            urlMock.Setup(u => u.Action(It.IsAny<UrlActionContext>())).Returns(callBackUrl);

            await controller.ResendEmailConfirmation();

            mediator.Verify(m => m.SendAsync(It.Is<SendConfirmAccountEmail>(e => e.Email == user.Email && e.CallbackUrl == callBackUrl)));
        }

        [Fact]
        public async Task ResendEmailConfirmationRedirectsToCorrectAction()
        {
            var user = new ApplicationUser { Id = "MyUserID", Email = "me@email.com" };
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            userManagerMock.Setup(u => u.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync("");

            var mediator = new Mock<IMediator>();

            var controller = new ManageController(userManagerMock.Object, null, mediator.Object);
            controller.SetFakeUser(user.Id);
            var urlMock = new Mock<IUrlHelper>();
            controller.Url = urlMock.Object;
            urlMock.Setup(u => u.Action(It.IsAny<UrlActionContext>())).Returns("callbackUrl");

            var result = (RedirectToActionResult)await controller.ResendEmailConfirmation();

            Assert.Equal(result.ActionName, nameof(controller.EmailConfirmationSent));
        }

        [Fact]
        public void ResendEmailConfirmationHasHttpPostAttribute()
        {
            var controller = new ManageController(null, null, null);

            var attribute = controller.GetAttributesOn(x => x.ResendEmailConfirmation()).OfType<HttpPostAttribute>().SingleOrDefault();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void ResendEmailConfirmationHasHttpValidateAntiForgeryTokenAttribute()
        {
            var controller = new ManageController(null, null, null);

            var attribute = controller.GetAttributesOn(x => x.ResendEmailConfirmation()).OfType<ValidateAntiForgeryTokenAttribute>().SingleOrDefault();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void EmailConfirmationSentReturnsAView()
        {
            var controller = new ManageController(null, null, null);

            var result = controller.EmailConfirmationSent();

            Assert.IsType<ViewResult>(result);
        }

        [Fact(Skip = "NotImplemented")]
        public async Task RemoveLoginSendsUserByUserIdQueryWithCorrectUserId()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task RemoveLoginInvokesRemoveLoginAsyncWithCorrectParametersWhenUserIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task RemoveLoginInvokesSignInAsyncWithCorrectParametersWhenUserIsNotNullAndRemoveLoginSucceeds()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task RemoveLoginRedirectsToCorrectActionWithCorrectRouteValues()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact]
        public void RemoveLoginHasHttpPostAttribute()
        {
            var controller = new ManageController(null, null, null);

            var attribute = controller.GetAttributesOn(x => x.RemoveLogin("","")).OfType<HttpPostAttribute>().SingleOrDefault();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void RemoveLoginHasValidateAntiForgeryTokenAttribute()
        {
            
            var controller = new ManageController(null, null, null);
            
            var attribute = controller.GetAttributesOn(x => x.RemoveLogin("", "")).OfType<ValidateAntiForgeryTokenAttribute>().SingleOrDefault();
            
            Assert.NotNull(attribute);
        }

        [Fact]
        public async Task EnableTwoFactorAuthenticationSendsUserByUserIdQueryWithCorrectUserId()
        {
            const string UserId = "userID";
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(U => U.SetTwoFactorEnabledAsync(It.IsAny<ApplicationUser>(), true)).ReturnsAsync(new IdentityResult());
            userManagerMock.Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(UserId);

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);
            signInManagerMock.Setup(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null)).Returns(Task.FromResult(new ApplicationUser { Id = UserId }));

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser { Id = UserId });

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser(UserId);

            await controller.EnableTwoFactorAuthentication();

            mediator.Verify(u => u.SendAsync(It.Is<UserByUserIdQuery>(i => i.UserId == UserId)), Times.Once);
        }

        [Fact]
        public async Task EnableTwoFactorAuthenticationInvokesSetTwoFactorEnabledAsyncWhenUserIsNotNull()
        {
            const string userId = "userID";
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(U => U.SetTwoFactorEnabledAsync(It.IsAny<ApplicationUser>(), true)).ReturnsAsync(new IdentityResult());

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);
            signInManagerMock.Setup(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null)).Returns(Task.FromResult(new ApplicationUser { Id = userId }));

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser { Id= userId });

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser(userId);

            await controller.EnableTwoFactorAuthentication();

            userManagerMock.Verify(u => u.SetTwoFactorEnabledAsync(It.Is<ApplicationUser>(i => i.Id == userId),true), Times.Once);
        }

        [Fact]
        public async Task EnableTwoFactorAuthenticationInvokesSignInAsyncWhenUserIsNotNull()
        {
            const string userId = "userID";
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(U => U.SetTwoFactorEnabledAsync(It.IsAny<ApplicationUser>(), true)).ReturnsAsync(new IdentityResult()); ;

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);
            signInManagerMock.Setup(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null)).Returns(Task.FromResult(new ApplicationUser { Id = userId }));

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser { Id = userId });

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser(userId);

            await controller.EnableTwoFactorAuthentication();

            signInManagerMock.Verify(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(),null), Times.Once);
        }

        [Fact]
        public async Task EnableTwoFactorAuthenticationRedirectsToCorrectAction()
        {
            const string userId = "userID";

            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(U => U.SetTwoFactorEnabledAsync(It.IsAny<ApplicationUser>(), true)).ReturnsAsync(new IdentityResult()); ;

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);
            signInManagerMock.Setup(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null)).Returns(Task.FromResult(new ApplicationUser { Id = userId}));

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser { Id = userId });

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser(userId);

            var result = (RedirectToActionResult)await controller.EnableTwoFactorAuthentication();

            Assert.Equal(result.ActionName, nameof(controller.Index));
        }

        [Fact]
        public void EnbaleTwoFactorAuthenticationHasHttpPostAttribute()
        {
            var controller = new ManageController(null, null, null);

            var attribute = controller.GetAttributesOn(x => x.EnableTwoFactorAuthentication()).OfType<HttpPostAttribute>().SingleOrDefault();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void EnableTwoFactorAuthenticationHasValidateAntiForgeryTokenAttribute()
        {
            
            var controller = new ManageController(null, null, null);
            
            var attribute = controller.GetAttributesOn(x => x.EnableTwoFactorAuthentication()).OfType<ValidateAntiForgeryTokenAttribute>().SingleOrDefault();
            
            Assert.NotNull(attribute);
        }

        [Fact]
        public async Task DisableTwoFactorAuthenticationSendsUserByUserIdQueryWithCorrectUserId()
        {
            const string userId = "UserID";
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(U => U.SetTwoFactorEnabledAsync(It.IsAny<ApplicationUser>(), true)).ReturnsAsync(new IdentityResult());
            userManagerMock.Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);
            signInManagerMock.Setup(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null)).Returns(Task.FromResult(new ApplicationUser { Id = userId }));

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser { Id = userId });

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser(userId);
      
            await controller.DisableTwoFactorAuthentication();

            mediator.Verify(u => u.SendAsync(It.Is<UserByUserIdQuery>(i => i.UserId == userId)), Times.Once);
        }

        [Fact]
        public async Task DisableTwoFactorAuthenticationInvokesSetTwoFactorEnabledAsyncWithCorrectParametersWhenUserIsNotNull()
        {
            const string userId = "UserID";

            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(U => U.SetTwoFactorEnabledAsync(It.IsAny<ApplicationUser>(), false)).ReturnsAsync(new IdentityResult());

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);
            signInManagerMock.Setup(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null)).Returns(Task.FromResult(new ApplicationUser { Id = userId }));

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser { Id = userId });

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser(userId);

            await controller.DisableTwoFactorAuthentication();

            userManagerMock.Verify(u => u.SetTwoFactorEnabledAsync(It.Is<ApplicationUser>(i => i.Id == userId), false), Times.Once);
        }

        [Fact]
        public async Task DisableTwoFactorAuthenticationInvokesSignInAsyncWhenUserIsNotNull()
        {
            const string userId = "userID";

            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(U => U.SetTwoFactorEnabledAsync(It.IsAny<ApplicationUser>(), false)).ReturnsAsync(new IdentityResult()); ;

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);
            signInManagerMock.Setup(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null)).Returns(Task.FromResult(new ApplicationUser { Id = userId }));

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser { Id = userId });

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser(userId);

            await controller.DisableTwoFactorAuthentication();

            signInManagerMock.Verify(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null), Times.Once);
        }

        [Fact]
        public async Task DisableTwoFactorAuthenticationRedirectsToCorrectAction()
        {
            const string userId = "userID";
            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(U => U.SetTwoFactorEnabledAsync(It.IsAny<ApplicationUser>(), false)).ReturnsAsync(new IdentityResult()); ;

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);
            signInManagerMock.Setup(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null)).Returns(Task.FromResult(new ApplicationUser { Id = userId }));

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser { Id = userId });

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser(userId);

            var result = (RedirectToActionResult)await controller.DisableTwoFactorAuthentication();

            Assert.Equal(result.ActionName, nameof(controller.Index));
        }

        [Fact]
        public void DisableTwoFactorAuthenticationHasHttpPostAttribute()
        {
            
            var controller = new ManageController(null, null, null);
            
            var attribute = controller.GetAttributesOn(x => x.DisableTwoFactorAuthentication()).OfType<HttpPostAttribute>().SingleOrDefault();
            
            Assert.NotNull(attribute);
        }

        [Fact]
        public void DisableTwoFactorAuthenticationHasValidateAntiForgeryTokenAttribute()
        {
            var controller = new ManageController(null, null, null);

            var attribute = controller.GetAttributesOn(x => x.DisableTwoFactorAuthentication()).OfType<ValidateAntiForgeryTokenAttribute>().SingleOrDefault();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void ChangePasswordGetReturnsAView()
        {
            var controller = new ManageController(null, null, null);

            var result = controller.ChangePassword();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ChangePasswordGetHasHttpGetAttribute()
        {
            var controller = new ManageController(null, null, null);

            var attribute = controller.GetAttributesOn(x => x.ChangePassword()).OfType<HttpGetAttribute>().SingleOrDefault();

            Assert.NotNull(attribute);
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangePasswordPostReturnsSameViewAndModelWhenModelStateIsInvalid()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact]
        public async Task ChangePasswordPostSendsUserByUserIdQueryWithCorrectUserId()
        {
            const string userId = "UserID";
            var validVm = new ChangePasswordViewModel { OldPassword = "oldPassword", NewPassword = "newPassword" };

            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(u => u.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new IdentityResult());
            userManagerMock.Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);
            signInManagerMock.Setup(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null)).Returns(Task.FromResult(new ApplicationUser { Id = userId }));

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(new ApplicationUser { Id = userId });


            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser(userId);

            await controller.ChangePassword(validVm);

            mediator.Verify(u => u.SendAsync(It.Is<UserByUserIdQuery>(i => i.UserId == userId)), Times.Once);
        }

        [Fact]
        public async Task ChangePasswordPostInvokesChangePasswordAsyncWithCorrectParametersWhenUserIsNotNull()
        {
            var validVm = new ChangePasswordViewModel { OldPassword = "oldPassword", NewPassword = "newPassword" };

            var userManagerMock = UserManagerMockHelper.CreateUserManagerMock();
            userManagerMock.Setup(u => u.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new IdentityResult());
            userManagerMock.Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("userID");

            var signInManagerMock = SignInManagerMockHelper.CreateSignInManagerMock(userManagerMock);
            ApplicationUser user = new ApplicationUser { Id = "userID" };
            signInManagerMock.Setup(s => s.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null)).Returns(Task.FromResult(user));

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.SendAsync(It.IsAny<UserByUserIdQuery>())).ReturnsAsync(user);

            var controller = new ManageController(userManagerMock.Object, signInManagerMock.Object, mediator.Object);
            controller.SetFakeUser("userID");

            await controller.ChangePassword(validVm);

            userManagerMock.Verify(u => u.ChangePasswordAsync(It.Is<ApplicationUser>(usr => usr == user), validVm.OldPassword, validVm.NewPassword));
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangePasswordPostInvokesSignInAsyncWithCorrectParametersWhenUserIsNotNullAndPasswordWasChangedSuccessfully()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangePasswordPostRedirectsToCorrectActionWithCorrectRouteValuesWhenUserIsNotNullAndPasswordWasChangedSuccessfully()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangePasswordPostAddsIdentityResultErrorsToModelStateErrorsWhenUserIsNotNullAndPasswordWasNotChangedSuccessfully()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangePasswordPostReturnsCorrectViewModelWhenUserIsNotNullAndPasswordWasNotChangedSuccessfully()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangePasswordPostRedirectsToCorrectActionWithCorrectRouteValuesWhenUserIsNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public void ChangePasswordHPostasHttpPostAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void ChangePasswordPostHasValidateAntiForgeryTokenAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void ChangeEmailGetReturnsAView()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void ChangeEmailGetHasHttpGetAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostReturnsSameViewAndViewModelWhenModelStateIsInvalid()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostSendsUserByUserIdQueryWithCorrectUserId()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostInvokesCheckPasswordAsyncWithCorrectParametersWhenUserIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostAddsCorrectErrorMessageToModelStateWhenChangePasswordIsUnsuccessful()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostReturnsCorrectVieModelWhenChangePasswordIsUnsuccessful()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostInvokesFindByEmailAsyncWithCorrectParametersWhenChangePasswordIsSuccessful()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostAddsCorrectErrorToModelStateWhenChangePasswordIsSuccessfulAndEmailCannotBeFound()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostReturnsCorrectViewModelWhenChangePasswordIsSuccessfulAndEmailCannotBeFound()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostInvokesUpdateAsyncWithCorrectParametersWhenUserIsNotNullAndChangePasswordIsSuccessfulAndUsersEmailIsFound()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostInvokesGenerateChangeEmailTokenAsyncWithCorrectParametersWhenUserIsNotNullAndChangePasswordIsSuccessfulAndUsersEmailIsFound()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostInvokesUrlActioncWithCorrectParametersWhenUserIsNotNullAndChangePasswordIsSuccessfulAndUsersEmailIsFound()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostSendsSendNewEmailAddressConfirmationEmailAsyncWithCorrectDataWhenUserIsNotNullAndChangePasswordIsSuccessfulAndUsersEmailIsFound()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostRedirectsToCorrectActionWithCorrectRouteValuesWhenUserIsNotNullAndChangePasswordIsSuccessfulAndUsersEmailIsFound()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ChangeEmailPostRedirectsToTheCorrectActionWithTheCorrectRouteValuesWhenUserIsNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public void ChangeEmailPostHasHttpPostAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void ChangeEmailPostHasValidateAntiForgeryTokenAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ConfirmNewEmailReturnsErrorViewWhenTokenIsNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ConfirmNewEmailSendsUserByUserIdQueryWithCorrectUserId()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ConfirmNewEmailReturnsErrorViewWhenUserIsNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ConfirmNewEmailInvokesChangeEmailAsyncWithCorrectParametersWhenUserIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ConfirmNewEmailInvokesSetUserNameAsyncWithCorrectParametersWhenUserIsNotNullAndSettingUserNameIsSuccessful()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ConfirmNewEmailInvokesUpdateAsyncWithCorrectParametersWhenUserIsNotNullAndSettingUserNameIsSuccessful()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ConfirmNewEmailRedirectsToCorrectActionWithCorrectRouteValues()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public void ConfirmEmailHasHttpGetAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ResendChangeEmailConfirmationSendsUserByUserIdQueryWithCorrectUserId()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ResendChangeEmailConfirmationReturnsErrorViewWhenUsersPendingNewEmailIsNullOrEmpty()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ResendChangesEmailConfirmationInvokesGenerateChangeEmailTokenAsyncWithCorrectParameters()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ResendChangesEmailConfirmationInvokesUrlActionWithCorrectParameters()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        //public async Task ResendChangesEmailConfirmationInvokesSendEmailAsyncWithCorrectParameters()
        public async Task ResendChangesEmailConfirmationSendsSendNewEmailAddressConfirmationEmailAsyncWithCorrectData()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ResendChangesEmailConfirmationRedirectsToCorrectAction()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public void ResendChangesEmailConfirmationHasHttpPostAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void ResendChangesEmailConfirmationHasVAlidateAntiForgeryTokenAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public async Task CancelChangeEmailSendsUserByUserIdQueryWithCorrectUserId()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task CancelChangeEmailInvokesUpdateAsyncWithCorrectParameters()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task CancelChangeEmailRedirectsToCorrectAction()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public void CancelChangeEmailHasHttpPostAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void CancelChangeEmailHasValidateAntiForgeryTokenAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void SetPasswordGetReturnsAView()
        {   
        }

        [Fact(Skip = "NotImplemented")]
        public void SetPasswordGetHasHttpGetAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public async Task SetPasswordPostReturnsSameViewAndViewModelWhenModelStateIsInvalid()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task SetPasswordPostSendsUserByUserIdQueryWithCorrectUserId()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task SetPasswordPostInvokesAddPasswordAsyncWithCorrectParametersWhenUserIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task SetPasswordPostInvokesSignInAsyncWithCorrectParametersWhenUserIsNotNullAndPasswordAddedSuccessfully()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task SetPasswordPostRedirectsToCorrectActionWithCorrectRouteValuesWhenUserIsNotNullAndPasswordAddedSuccessfully()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task SetPasswordPostAddsCorrectErrorMessageToModelStateWhenUserIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task SetPasswordPostReturnsCorrectViewModelWhenUserIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task SetPasswordPostRedirectsToCorrectActionWithCorrectRouteValuesWhenUserIsNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public void SetPasswordPostHasHttpPostAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void SetPasswordPostHasValidateAntiForgeryTokenAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ManageLoginsAddsCorrectMessageToViewDataWhenMessageIdIsRemoveLoginSuccess()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ManageLoginsAddsCorrectMessageToViewDataWhenMessageIdIsAddLoginSuccess()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ManageLoginsAddsCorrectMessageToViewDataWhenMessageIdIsError()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ManageLoginsSendsUserByUserIdQueryWithCorrectUserId()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ManageLoginsReturnsErrorViewWhenUserIsNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ManageLoginsInvokesGetLoginsAsyncWithCorrectParametersWhenUserIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ManageLoginsInvokesGetExternalAuthenticationSchemesWhenUserIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ManageLoginsAddsCorrectValueToSHOW_REMOVE_BUTTONWhenUserIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ManageLoginsReturnsCorrectViewModelWhenUserIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public void ManageLoginsHasHttpGetAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void LinkLoginInvokesUrlActionWithTheCorrectParameters()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void LinkLoginInvokesConfigureExternalAuthenticationPropertiesWithCorrectParameters()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void LinkLoginReturnsCorrectResult()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void LinkLoginHasHttpPostAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public void LinkLoginHasValidateAntiForgeryTokenAttribute()
        {
        }

        [Fact(Skip = "NotImplemented")]
        public async Task LinkLoginCallbackSendsUserByUserIdQueryWithCorrectUserId()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task LinkLoginCallbackReturnsErrorViewWhenUserIsNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task LinkLoginCallbackInvokesGetExternalLoginInfoAsyncWithCorrectUserIdWhenUserIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task LinkLoginCallbackRedirectsToCorrectActionWithCorrectRouteValuesWhenUserIsNotNullAndExternalLoginInfoIsNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task LinkLoginCallbackInvokesAddLoginAsyncWithCorrectParametersWhenUserIsNotNullAndExternalLoginInfoIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task LinkLoginCallbackRedirectsToCorrectActionWithCorrectRouteParametersWhenUserIsNotNullAndExternalLoginInfoIsNotNull()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }

        [Fact(Skip = "NotImplemented")]
        public async Task ControllerHasAuthorizeAtttribute()
        {
            //delete this line when starting work on this unit test
            await TaskCompletedTask;
        }
    }
}