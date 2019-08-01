# CryptoBasket
E-Commerce crypto basket api

## Build status [![Build Status](https://travis-ci.org/ErickGallani/CryptoBasket.svg?branch=master)](https://travis-ci.org/ErickGallani/CryptoBasket)

#### Test coverage status
![Test coverage](/assets/coverage.PNG) 

## Architectural choice
- The architeture used for this project was the ONION architecture

![Onion Example 1](/assets/onion_example_1.png)  

![Architectural overview](/assets/Architectural_Overview.png)  

## Patterns used
- S.O.L.I.D
- DRY
- YAGNI
- KISS

## Technologies used
- Aps.Net Core 2.2
- Swagger for api documentation (Swashbuckle)
- Logs using Serilog
- Docker
- Polly for Http resilience

# Resilience
- Polly

![The polly project](/assets/the_polly_project.PNG)  

For the Http requests resilience, Polly was choose for the purpose of this example with only a retry police `AddTransientHttpErrorPolicy` -> `WaitAndRetryAsync`.

```
services.AddHttpClient(HttpConsts.HTTP_NAME, c =>
{
    c.BaseAddress = new Uri("https://pro-api.coinmarketcap.com/");
    
    c.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "my-key");
    c.DefaultRequestHeaders.Add("Accept", "application/json");
})
.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
{
    TimeSpan.FromSeconds(1),
    TimeSpan.FromSeconds(5),
    TimeSpan.FromSeconds(10)
}));
```

## Continous integration
- For the continous integration and deployment was used **travis-ci** platform. Travis-ci is a great CI/CD platform free for open source project, more information at https://travis-ci.com/.

## Api design principles
- Restfull
- Versioning
- Authentication
- HATEOAS

## Authorization process swagger
![Authorization swagger](/assets/authorize_process.PNG)

## To build your own image
- docker build -t myimagename .

## To run the container (example)
- docker run -d -p 8097:80 myimagename
- access on browser http://localhost:8097/swagger/index.html