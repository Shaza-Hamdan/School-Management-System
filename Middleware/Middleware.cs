// using TRIAL.Persistence.Repository;
// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.IdentityModel.Tokens;

// namespace TRIAL.Middleware
// {
//     public class RoleMiddleware : IMiddleware
//     {
//         private readonly AppDBContext dbContext;

//         public RoleMiddleware(AppDBContext appdbContext)
//         {
//             dbContext = appdbContext;
//         }

//         public async Task InvokeAsync(HttpContext context, RequestDelegate next) //the core method that is called for each incoming HTTP request
//         {
//             var path = context.Request.Path.ToString();
//             string token = context.Request.Headers["Authorization"];//The Authorization header is read, which usually contains the user's access token

//             if (string.IsNullOrEmpty(token))
//             {
//                 context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//                 await context.Response.WriteAsync("Access token is required");
//                 return;
//             }

//             // Decrypt token and get user information
//             var email = GetEmailFromToken(token);
//             var user = dbContext.registrations.SingleOrDefault(u => u.Email == email);

//             if (user == null)
//             {
//                 context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//                 await context.Response.WriteAsync("User not found");
//                 return;
//             }

//             // Check if the route matches a role's specific URL
//             if (path.Contains("/api/student") && user.Role != "student")
//             {
//                 context.Response.StatusCode = StatusCodes.Status403Forbidden;
//                 await context.Response.WriteAsync("Only students can access this.");
//                 return;
//             }

//             if (path.Contains("/api/teacher") && user.Role != "teacher")
//             {
//                 context.Response.StatusCode = StatusCodes.Status403Forbidden;
//                 await context.Response.WriteAsync("Only teachers can access this.");
//                 return;
//             }

//             if (path.Contains("/api/admin") && user.Role != "admin")
//             {
//                 context.Response.StatusCode = StatusCodes.Status403Forbidden;
//                 await context.Response.WriteAsync("Only admins can access this.");
//                 return;
//             }

//             // Continue with the next middleware in the pipeline
//             await next(context);
//         }

//         private string GetEmailFromToken(string token)
//         {
//             //Assuming the token is a JWT, we can decode it to get the email
//             var handler = new JwtSecurityTokenHandler();//a class in the .NET framework that provides functionality to read, write, and validate JWTs.

//             // Check if token is valid
//             if (handler.CanReadToken(token))//helps verify that the token is properly formatted and readable.
//             {
//                 var jwtToken = handler.ReadJwtToken(token);//parses the token into its parts: header, payload (claims), and signature.

//                 // Extract the email from the JWT claims
//                 var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "email");

//                 if (emailClaim != null)
//                 {
//                     return emailClaim.Value;
//                 }
//             }

//             throw new SecurityTokenException("Invalid token");
//         }
//     }
// }
