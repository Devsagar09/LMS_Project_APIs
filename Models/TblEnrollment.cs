using System;
using System.Collections.Generic;

namespace LMS_Project_APIs.Models;

public partial class TblEnrollment
{
    public int EnrollId { get; set; }

    public int? StudentId { get; set; }

    public int? TrainingId { get; set; }

    public DateTime? EnrollDate { get; set; }

    public virtual TblStudent? Student { get; set; }
}
