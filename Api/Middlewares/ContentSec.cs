/*
* This class contains configuration of middlewares for : 
*
* 1. Content security Policy (CSP)
* 2. Security Headers
*/

namespace Api.Middlewares;

public class ContentSecHeaders(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        // Adding security headers
        context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains"); // HSTS
        context.Response.Headers.Append("X-Frame-Options", "DENY"); // clickjacking
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff"); // bad MIME interpretation 
        context.Response.Headers.Append("Referrer-Policy", "no-referrer"); // Referrer

        // Adding CSP header
        context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'; script-src 'self'; style-src 'self'; img-src 'self' data:;");

        await _next(context);
    }
}