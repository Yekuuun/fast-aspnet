using System.Threading.RateLimiting;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
var handler = new HttpClientHandler
{
    UseProxy = true,
    // Autres configurations de proxy
};

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//lower case for all controllers.
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

//MAPPER
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//SERVICES
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();

//REPOSITORIES
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PostRepository>();

/*
* CORS POLICY => MUST BE CONFIGURED FOR YOUR OWN CAS => THIS IS A SAMPLE DEMO.
*/
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
            {
                //authorize access from api gateway
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowCredentials();
            });
});

/*
* Adding a rate limiting base on IP => avoiding request spamming.
* NOTES : you can config your own permitLimit & window size. => THIS IS A SAMPLE.
*/
builder.Services.AddRateLimiter(options =>
{
    _ = options.AddPolicy("fixed", httpContext =>
    RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
        factory: partition => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 25,
            Window = TimeSpan.FromMinutes(5)
        }
    ));
    
    // Utilisation de l'adresse IP comme identifiant de l'utilisateur
    options.RejectionStatusCode = 429;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*
* Rate limiter.
*/
app.UseRateLimiter();

/*
* Adding HTTP secure headers.
*/
app.UseMiddleware<ContentSecHeaders>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();