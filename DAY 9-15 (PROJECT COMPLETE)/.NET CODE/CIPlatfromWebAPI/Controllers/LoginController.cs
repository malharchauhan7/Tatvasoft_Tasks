using Business_logic_Layer;
using Data_Access_Layer.Repository.Entities;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {       
        private readonly BALLogin _balLogin;
        ResponseResult result = new ResponseResult();
        public LoginController(BALLogin balLogin)
        {           
            _balLogin = balLogin;
        }


        /*[HttpPost]
        [Route("LoginUser")]
        public ResponseResult LoginUser(User user)
        {
            try
            {                                
                result.Data = _balLogin.LoginUser(user);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }*/
        [HttpPost]
        [Route("LoginUser")]
        public ResponseResult LoginUser(LoginUserModel user)
        {
            try
            {
                result.Data = _balLogin.LoginUser(user);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("Register")]
        public ResponseResult RegisterUser(AddUserModel user)
        {
            try
            {
                result.Data = _balLogin.Register(user);
                result.Result = ResponseStatus.Success;
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
            }
            return result;
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<ActionResult<ResponseResult>> GetUserById(int id)
        {
            try
            {
                result.Data = await _balLogin.GetUserByIdAsync(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<ActionResult<ResponseResult>> UpdateUser(UpdateUserModel user)
        {
            try
            {
                result.Data = await _balLogin.UpdateUserAsync(user);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }


        [HttpPost]
        [Route("LoginUserProfileUpdate")]
        public async Task<ActionResult<ResponseResult>> LoginUserProfileUpdate(UserDetail userDetail)
        {
            try
            {
                result.Data = await _balLogin.LoginUserProfileUpdate(userDetail);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("GetUserProfileDetailById/{userId}")]
        public async Task<ActionResult<ResponseResult>> GetUserProfileDetailById(int userId)
        {
            try
            {
                result.Data = await _balLogin.GetUserProfileDetailById(userId);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("LoginUserDetailById/{id}")]
        public async Task<ActionResult<ResponseResult>> LoginUserDetailById(int id)
        {
            try
            {
                result.Data = await _balLogin.GetUserProfileDetailById(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ResponseResult> ChangePassword(ChangePassModel changePass)
        {
            try
            {
                result.Data = await _balLogin.ChangePassword(changePass);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

    }
}
