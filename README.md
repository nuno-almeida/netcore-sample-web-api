
# NETCore Sample Web API

This repo contains a sample NETCore REST API which uses PostgreSQL, Kafka, Redis and MongoDB:<br />

<ul>
<li><b>Students Controller</b> implements CRUD operations of Students.<br />
https://github.com/nuno-almeida/netcore.sample.web.api/blob/main/netcore.sample.web.api/Controllers/StudentsController.cs
</li>
<br />
<li><b>PostgreSQL</b> is used to store Students. The access to database is through Entity Framework.<br />
  https://github.com/nuno-almeida/netcore.sample.web.api/blob/main/netcore.sample.web.api/Configurations/StudentContext.cs<br />
  https://github.com/nuno-almeida/netcore.sample.web.api/blob/main/netcore.sample.web.api/Services/StudentService.cs
</li>
<br />
<li><b>MongoDB</b> to store Audits records which represent access to each Students controller endpoint.<br />
  https://github.com/nuno-almeida/netcore.sample.web.api/blob/main/netcore.sample.web.api/Services/AuditRepository.cs
</li>
<br />
<li><b>Kafka</b> there is a filter which intercepts each Students controller endpoints requests and sends to a Kafka topic info about the operation (Read, Write, Delete). The Kafka consumer on that topic creates the Audit record in the MongoDB.<br />
https://github.com/nuno-almeida/netcore.sample.web.api/blob/main/netcore.sample.web.api/Filters/AuditFilter.cs
  https://github.com/nuno-almeida/netcore.sample.web.api/blob/main/netcore.sample.web.api/Services/KafkaProducer.cs
  https://github.com/nuno-almeida/netcore.sample.web.api/blob/main/netcore.sample.web.api/Configurations/AuditKafkaConsumer.cs
</li>
<br />
<li><b>Redis</b> is used to implement a simple Rate Limiter in Students controller.<br />
  https://github.com/nuno-almeida/netcore.sample.web.api/blob/main/netcore.sample.web.api/Filters/RateLimiterFilter.cs
  https://github.com/nuno-almeida/netcore.sample.web.api/blob/main/netcore.sample.web.api/Services/RedisService.cs
</li>
</ul>

### Run locally

Before run the project locally, run the command `docker compose up postgres mongo redis zookeeper kafka` to start the required services.
