namespace GraphQL.Api.Schema.Subscriptions;

public class Subscription
{
    [Subscribe]
    public CourseType CourseCreated([EventMessage] CourseType course) => course;
}