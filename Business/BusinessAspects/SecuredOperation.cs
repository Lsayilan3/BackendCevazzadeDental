using Business.Constants;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace Business.BusinessAspects
{
    /// <summary>
    /// This Aspect control the user's roles in HttpContext by inject the IHttpContextAccessor.
    /// It is checked by writing as [SecuredOperation] on the handler.
    /// If a valid authorization cannot be found in aspect, it throws an exception.
    /// </summary>

    public class SecuredOperation : MethodInterception
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICacheManager _cacheManager;
        private readonly IUserRepository _users;   // burası cachleri getiremesse veri tabanından getirmek için ayarlandı


        public SecuredOperation()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            _users = ServiceTool.ServiceProvider.GetService<IUserRepository>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                if (userId == null)
                {
                    throw new SecurityException(Messages.AuthorizationsDenied);
                }

                // Cache'den veri almayı atla ve doğrudan veritabanından al
                var oprClaims = _users.GetClaims(int.Parse(userId)).Select(s => s.Name).ToList();

                var operationName = invocation.TargetType.ReflectedType.Name ?? "";

                if (oprClaims.Contains(operationName))
                {
                    return;
                }

                throw new SecurityException(Messages.AuthorizationsDenied);
            }
            catch (System.Exception e)
            {
                throw new System.Exception("SecuredOperation:" + e.Message);
            }
        }
    }
}
