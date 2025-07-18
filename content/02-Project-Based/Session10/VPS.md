ASP.NET CORE Setup -> 
1. Make sure everything works and builds successfully
2. SetUp :
	1. add env files to git ignore
	2. Create env file
	3. install DotNetEnv
	4. Add 
```
DotNetEnv.Env.Load();
```
	  5. Update stuff:
  ```
  var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
  ...
builder.Services.Configure<JwtSettings>(options =>
{
    options.Key = Environment.GetEnvironmentVariable("JWT_KEY");
    options.Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
    options.Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
    options.ExpiryMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES") ?? "60");
});

JwtSettings jwtSettings = new JwtSettings
{
    Key = Environment.GetEnvironmentVariable("JWT_KEY"),
    Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
    Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
    ExpiryMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES") ?? "60"),
};


var corsOrigin = Environment.GetEnvironmentVariable("CORS_ORIGIN");
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins(corsOrigin)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
  ```

```
CORS_ORIGIN=http://localhost:5173
CONNECTION_STRING=Server=SERVER;Database=DB;Trusted_Connection=True;TrustServerCertificate=True
JWT_KEY=KEY
JWT_ISSUER=ISSUER
JWT_AUDIENCE=MyAppUsers
JWT_EXPIRY_MINUTES=360 
```

6. Update README and tell about environment variables
	


---
How is this dockerfile created?    
How is it updated? with push and stuff?
What about CORS? how do I know which port?
What about connection string? in appsettings, how is that gonna be set?
How is it gonna be running all the time?

---
How to have seed data?
How is the database created?
How is it updated?
Do I upload a backup, script? 
Is it done with migrations?

---

What is the address in the frontend api is set? 





