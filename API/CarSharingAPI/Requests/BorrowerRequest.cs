﻿using System;
using DataAccess.Enums;

namespace CarSharingAPI.Requests;

public record BorrowerRequest
{
    public int Id { get; set; }
    public DateTime Birth { get; set; }
    public string Country { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public LicenceCategory Category { get; set; }
    public DateTime LicenceExipiry { get; set; }
    public DateTime LicenceIssue { get; set; }
    public string LicenceId { get; set; }
    public string PlaceOfIssue { get; set; }
}