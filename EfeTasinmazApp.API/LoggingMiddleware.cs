using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Tasinmaz_Proje.Entities;
using Tasinmaz_Proje.Services;
using System;
using System.Security.Claims;

namespace Tasinmaz_Proje.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logService;

        public LoggingMiddleware(RequestDelegate next, ILogService logService)
        {
            _next = next;
            _logService = logService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userIp = context.Connection.RemoteIpAddress?.ToString();
            var requestPath = context.Request.Path;
            var method = context.Request.Method;
            var statusCode = context.Response.StatusCode;

            var log = new Log
            {
                KullaniciId = userId != null ? int.Parse(userId) : 0,
                Durum = statusCode == 200 ? "Başarılı" : "Başarısız",
                IslemTip = method,
                Aciklama = $"{method} request to {requestPath} - Status code: {statusCode}",
                TarihveSaat = DateTime.Now,
                KullaniciTip = userIp
            };

            await _logService.AddAsync(log);
        }
    }
}
