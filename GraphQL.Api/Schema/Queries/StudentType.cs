﻿namespace GraphQL.Api.Schema.Queries;

public class StudentType
{
    
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    [GraphQLName("gpa")]
    public double GPA { get; set; }
}