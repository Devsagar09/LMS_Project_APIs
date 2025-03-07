using System;
using System.Collections.Generic;

namespace LMS_Project_APIs.Models;

public partial class TblStudent
{
    public int StudentId { get; set; }

    public string? StudentNo { get; set; }

    public string? Firstname { get; set; }

    public string? Middlename { get; set; }

    public string? Lastname { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public int? RoleId { get; set; }

    public string? Role_name { get; set; }
    public string? ProfileImage { get; set; }

    public DateOnly? ArchiveDate { get; set; }

    public string? PhoneNo { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? PostalCode { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual TblRole? Role { get; set; }

    public virtual ICollection<TblApproval> TblApprovals { get; set; } = new List<TblApproval>();

    public virtual ICollection<TblAssign> TblAssigns { get; set; } = new List<TblAssign>();

    public virtual ICollection<TblTrainingTranscript> TblTrainingTranscripts { get; set; } = new List<TblTrainingTranscript>();

    public virtual ICollection<TblTraining> TblTrainings { get; set; } = new List<TblTraining>();
}
