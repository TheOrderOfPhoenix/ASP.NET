
# Add a field in Database
- [ ] Add `User` field in `Roles` table using SSMS or Seed data in DbContext file
- [ ] Make sure the property `PersonId` is nullable in `Account`, so you can add fields related to "Person" later after registration

# Branching
- [ ] Create the feature/authentication branch based on develop

# Adjusting Account and Configurations 

- [ ] Add navigation property for `AccountRoles` in Account
```c#
public virtual ICollection<AccountRole> AccountRoles { get; set; }
```

- [ ] Add navigation properties for Account and Role in `AccountRole`
```C#
public virtual Role Role { get; set; }
public virtual Account Account{ get; set; }
```

- [ ] Update the entity configuration to reflect relationship mappings:
```c#
builder.HasOne<Account>(ar => ar.Account)
    .WithMany(a => a.AccountRoles)
    .HasForeignKey(ar => ar.AccountId)
    .OnDelete(DeleteBehavior.Restrict);

builder.HasOne<Role>(ar => ar.Role)
    .WithMany()
    .HasForeignKey(ar => ar.RoleId)
    .OnDelete(DeleteBehavior.Restrict);
```

# Creating DTOs
📂 Suggested Folder: ApplicationLayer/DTOs/[RelatedFolder]
## AccountDto
- [ ] Create `AccountDto` to expose relevant account information:
```c#
public class AccountDto
{
    public long Id { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Password { set; get; }
    public string? Email { get; set; }
    public long? PersonId { get; set; }
    public List<string> Roles { get; set; }
}
```
## AuthResponseDto
- [ ] Define a DTO for authentication responses:
```c#
public class AuthResponseDto
{
    public long Id { get; set; }
    public string Token { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public List<string> Roles { get; set; }
}
```
- [ ] Define a DTO for login requests:
```c#
public class LoginRequestDto
{
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
}
```
## RegisterRequestDto
- [ ] Define a DTO for registration with validation attributes:
```c#
public class RegisterRequestDto
{
    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Phone number format is invalid.")]
    public required string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public required string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public required string ConfirmPassword { get; set; }
}
```


### 🔹 **1. What do the annotations like `[Required]`, `[Phone]`, `[MinLength]`, `[Compare]` on the DTO do?**

These are **Data Annotations** from `System.ComponentModel.DataAnnotations`.

They’re used by:

- The ASP.NET Core `[ApiController]` attribute
    
- **Model binding & automatic validation**
    

**What happens:**  
If your controller is marked with `[ApiController]`, ASP.NET Core will **automatically validate** the DTO against these annotations **before entering your action method**.

Example:

```csharp
[ApiController]
public class AuthController : ControllerBase
```

Then this:

```csharp
[HttpPost("register")]
public async Task<IActionResult> Register(RegisterRequestDto dto)
```

If `dto.PhoneNumber` is missing, it **won’t even run your logic**, and will return a `400 Bad Request` with validation errors.


> “Why are these here if my frontend is separate?”

✅ **Answer**: They're still useful:

- For **security and safety**: you _must_ validate on the backend — never trust the frontend.
    
- For **auto validation** before hitting your logic — saving you boilerplate checks.
    
- You can use them for **Swagger/OpenAPI documentation** as well.
    

Frontend validation is for **user experience**, not security.

---

### 🔹 **2. Where should password requirements be checked? Frontend or backend?**

✅ **Both.**

- **Frontend**: show real-time UX feedback (“Password must be 6+ characters”).
    
- **Backend**: enforce security.
    

**Backend is the source of truth.**  
Frontend can be bypassed (e.g., Postman).

In the backend, you can either:

- Use annotations like `[MinLength(6)]`
    
- Or do manual checks:
    

```csharp
if (dto.Password.Length < 6)
    return BadRequest("Password must be at least 6 characters long.");
```

---

### 🔹 **3. Should confirm password be in the backend?**

✅ **Yes — if you're doing password comparison in backend.**

- `[Compare("Password")]` will validate if `ConfirmPassword` matches.
    
- Otherwise, you’ll need to check manually.
    

You **can skip sending ConfirmPassword to backend** and just validate in frontend if you’re confident your frontend handles it.

But again: if someone sends malformed input manually (e.g., via Postman), backend should defend.

💡 **Best practice:**

- Validate `ConfirmPassword` in frontend (UX)
    
- Do one last check in backend, or use `[Compare]` for auto-validation
    

---

### 🔹 **4. Is it OK to send plain password in request? Or should we hash it on frontend?**

**✅ YES — it is OK and standard to send raw password in the login/signup request.**

Why?

- Passwords are sent over **HTTPS**, which encrypts the entire request.
    
- Hashing on frontend is _not_ secure, because:
    
    - Your algorithm/salt would be exposed
        
    - It defeats the purpose of salting and hashing correctly
        
    - You lose control over security management
        

### 🔹 **5. Error Response from Automatic Model Validation**

If your DTO looks like this:

```csharp
public class RegisterRequestDto
{
    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Phone number format is invalid.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
}
```

And the frontend sends this:

```json
{
  "phoneNumber": "",
  "password": "123",
  "confirmPassword": "abc"
}
```

#### The backend will automatically return:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "PhoneNumber": [
      "Phone number is required."
    ],
    "Password": [
      "Password must be at least 6 characters long."
    ],
    "ConfirmPassword": [
      "Passwords do not match."
    ]
  }
}
```

This is thanks to `[ApiController]` on your controller class. The framework uses the **ModelState** and returns errors in a structured way.

---



## Adding Mappings
- [ ] Update `MappingProfile` with the following mappings:

```c#
CreateMap<Account, AccountDto>()
    .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.AccountRoles.Select(x=>x.Role.Title)));
CreateMap<AccountDto, Account>() 
.ForMember(dest => dest.AccountRoles, opt => opt.Ignore());
```

# Add Password Hasher Utility
- [ ] Create a password hashing utility class
📂 Suggested Folder: ApplicationLayer/Utils/`PasswordHasher.cs`
```c#
public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);

        return Convert.ToBase64String(hashBytes);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        for (int i = 0; i < 32; i++)
        {
            if (hashBytes[i + 16] != hash[i])
                return false;
        }

        return true;
    }
}
```


# Modifying Account Repository 

- [ ] Add the following methods in **`IAccountRepository`** 

```c#
Task<Account> GetByPhoneNumberAsync(string phoneNumber);
Task AddAccountRoleAsync(AccountRole accountRole);
```

- [ ] Implement the methods in **`AccountRepository`**
```c#
 public async Task AddAccountRoleAsync(AccountRole accountRole)
 {
     await DbContext.AccountRoles.AddAsync(accountRole);
 }
 
 public async Task<Account> GetByPhoneNumberAsync(string phoneNumber)
 {
     var user = await DbContext.Accounts.Include(x => x.AccountRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
     return user;
 }
```

# Creating Service
## Fix `Result.cs` Error Method
- [ ] Update the `Error` method to include error messages:
```c#
public static Result<T> Error(T data, string errorMessage) => new() { Status = ResultStatus.Error, Data = data, ErrorMessage = errorMessage };
```

## Creating `IAuthService.cs` and `AuthService.cs`
- [ ] Define the `IAuthService` interface
```c#
public interface IAuthService
{
    Task<Result<AuthResponseDto>> RegisterAsync(RegisterRequestDto request);
    Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request);
}
```

- [ ] Implement the interface in `AuthService.cs`

Use this project as a reference: 
https://github.com/MehrdadShirvani/AlibabaClone-Backend/blob/develop/AlibabaClone.Application/Services/AuthService.cs

## Register `IAuthService` in Service in `Program.cs`

- [ ] Add to `Program.cs`
```c# 
//...
builder.Services.AddScoped<IAuthService, AuthService>();
//...
```


# Adding JWT

## Installing Required NuGet Packages
Install these packages in the **`WebApi` (Presentation Layer)** project:
```
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.IdentityModel.Tokens
System.IdentityModel.Tokens.Jwt
```

## Create JWT Configuration Classes

- [ ] Add JWT section to `appsettings.json`
```xaml
"Jwt": {
	"Key": "[supersecretkeyyoustoresecurely]",
	"Issuer": "[Issuer]",
	"Audience": "MyAppUsers",
	"ExpiryMinutes": 60
}
```
Note that you should fill the values as you wish - these are just samples

- [ ] Create  `JwtSettings` and add the following method
📂 Suggested Folder: WebAPI/Authentication
```c#
public class JwtSettings
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpiryMinutes { get; set; }
}
```

- [ ] Create  `IJwtGenerator` and add the following method
📂 Suggested Folder: WebAPI/Authentication
```c#
string GenerateToken(AuthResponseDto authResponseDto);
```


- [ ] Create  `JwtGenerator`, implementing `IJwtGenerator`
📂 Suggested Folder: WebAPI/Authentication

use this project as a reference
https://github.com/MehrdadShirvani/AlibabaClone-Backend/blob/develop/AlibabaClone.WebAPI/Authentication/JwtGenerator.cs


## Configuring Jwt in Program.cs
- [ ] Register `JwtGenerator` service
```c#
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
```
- [ ] Bind `JwtSettings` from configuration
```c#
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
```
- [ ] Configure JWT authentication
```c#
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

```

- [ ] Add `app.UseAuthentication` before `app.UseAuthorization`
```c#
app.UseAuthentication();
app.UseAuthorization();
```
# Add ApiControllers

- [ ] Create `AuthController`
📂 Suggested Folder: WebApi/Controllers/`AuthController.cs`

- [ ] Add Class and Constructor
```c#
private readonly IAuthService _authService;
private readonly IJwtGenerator _jwtGenerator;

public AuthController(IAuthService authService, IJwtGenerator jwtGenerator)
{
    _authService = authService;
    _jwtGenerator = jwtGenerator;
}
```
- [ ] Add Register Method
```c#
public async Task<IActionResult> Register(RegisterRequestDto request)
{
    var result = await _authService.RegisterAsync(request);

    if (!result.IsSuccess)
        return BadRequest(result.ErrorMessage);

    var token = _jwtGenerator.GenerateToken(result.Data);
    var response = new AuthResponseDto
    {
        PhoneNumber = result.Data.PhoneNumber,
        Roles = result.Data.Roles,
        Token = token
    };

    return Ok(response);
}
```
- [ ] Add Login Method

```c#
 [HttpPost("login")]
 public async Task<IActionResult> Login(LoginRequestDto request)
 {
     var result = await _authService.LoginAsync(request);

     if (!result.IsSuccess)
         return Unauthorized(result.ErrorMessage);

     var token = _jwtGenerator.GenerateToken(result.Data);
     var response = new AuthResponseDto
     {
         PhoneNumber = result.Data.PhoneNumber,
         Roles = result.Data.Roles,
         Token = token
     };

     return Ok(response);
 }
```

- [ ] Create AccountController
📂 Suggested Folder: WebApi/Controllers/AccountController.cs
```c#
public class AccountController : ControllerBase
{
    [Authorize(Roles = "User")]
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        return Ok("Hi there, hello");
    }
}
```

# Merge
- [ ] Create a PR and merge the current branch with develop



