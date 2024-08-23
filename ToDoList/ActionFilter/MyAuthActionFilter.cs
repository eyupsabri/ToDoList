using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Helpers;

namespace ToDoList.ActionFilter
{
    public class MyAuthActionFilter : IActionFilter
    {
        private readonly string _secretKey;
        private readonly string _jwtIssuer;


        public MyAuthActionFilter(string secretKey, string jwtIssuer)
        {
            _secretKey = secretKey;
            _jwtIssuer = jwtIssuer;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {

            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();


                var response = TokenHelper.ValidateToken(token, _secretKey, _jwtIssuer);
                if (!response.IsValidToken)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                if (response.IsExpired)
                {
                    if (response.TokenType == "access")
                    {
                        //unauthorized ve refresh token iste
                        context.Result = new ContentResult
                        {
                            StatusCode = 401,
                            Content = "Refresh Token",
                            ContentType = "text/plain",
                        };
                        return;
                    }

                }
                else
                {
                    if (response.TokenType == "access")
                    {
                        context.HttpContext.Items["Email"] = response.Email;
                        return;
                    }

                }
            }

            context.Result = new UnauthorizedResult();
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

    }
}

