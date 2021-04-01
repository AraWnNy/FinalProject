using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cahceManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cahceManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cahceManager.IsAdd(key))
            {
                invocation.ReturnValue = _cahceManager.Get(key);
                return;
            }

            invocation.Proceed();
            _cahceManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
