```                
                        ____           __                                    __ 
                       / __/___ ______/ /_      ____ __________  ____  ___  / /_
                      / /_/ __ `/ ___/ __/_____/ __ `/ ___/ __ \/ __ \/ _ \/ __/
                     / __/ /_/ (__  ) /_/_____/ /_/ (__  ) /_/ / / / /  __/ /_  
                    /_/  \__,_/____/\__/      \__,_/____/ .___/_/ /_/\___/\__/  
                                                       /_/
                                ---ASP.NET core WEB API using .NET8---                                                          
```

               
**fast-aspnet** is a pre-built ASP.NET core web API using .NET 8 with embedded security features &amp; good practices.

## Code Architecture :
Before delving into features, let's explain the code organization:

**MODELS:**
Models are used to define your future database entities. I've chosen to use BaseEntity models to centralize data logic, such as IDs and creation dates, for cleaner code.

**REPOSITORIES:**
Repositories are utilized to interact directly with the database using an abstraction layer for communication.

**SERVICES:**
Services are employed to manipulate client data using DTOs (Data Transfer Objects) and interact with the database using repository methods.

**CONTROLLERS:**
Controllers are responsible for interacting with your API through endpoints and retrieving data -> All controller are using **RESTFULL convention**

**MIGRATIONS :**
Migrations are used to interact with database creating your Models into it.

Creating migrations ? : `dotnet ef migrations add <migration_name>`

---

<img src="https://github.com/Yekuuun/fast-aspnet/blob/main/assets/api-controllers.png" alt="DebugInfo" />

---

## Sec features :
