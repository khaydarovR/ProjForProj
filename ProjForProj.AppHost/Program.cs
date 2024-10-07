var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ProjForProj_Api>("projforproj-api");

builder.Build().Run();
