# CORS (Backend Repository)
- [ ] open `program.cs` and add the following lines

```c#
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

...

app.UseCors("Frontend");
```

