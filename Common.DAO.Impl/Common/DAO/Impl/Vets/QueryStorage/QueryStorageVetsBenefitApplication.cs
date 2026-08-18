using System;

namespace TechnoPro.Common.DAO.Impl.Vets.QueryStorage
{
	// Token: 0x02000022 RID: 34
	internal static class QueryStorageVetsBenefitApplication
	{
		// Token: 0x0400003F RID: 63
		internal const string QS_VETS_STUDENT_CARD_INFO = "SELECT    vba.BenefitApplicationId,vba.PersonId,vba.ChapterId,vba.SemesterId,\r\n            vba.StudentAgreeCompleted,vba.BenAppCompleted,vba.RegistrationCompleted,\r\n            vba.PreferredStep,vba.FinalStatus,vba.CurrentProgressId,\r\n            vba.DateCreated,vba.DateLastModified,\r\n            s.SemesterTitle,s.StartDate,s.EndDate,\r\n            vc.ChapterTitle\r\nFROM        VetsBenefitApplication vba LEFT JOIN Semester s ON s.SemesterId=vba.SemesterId\r\n            LEFT JOIN VetsChapter vc ON vc.ChapterId=vba.ChapterId\r\nWHERE       vba.PersonId=@pid AND s.enddate>=getdate()\r\nORDER BY s.StartDate";

		// Token: 0x04000040 RID: 64
		internal const string QS_VETS_BENEFIT_APPLICATION_BY_ID = "SELECT    vba.BenefitApplicationId,vba.PersonId,vba.ChapterId,vba.SemesterId,\r\n            vba.PerSemesterId,\r\n            vba.StudentAgreeCompleted,vba.BenAppCompleted,vba.RegistrationCompleted,\r\n            vba.PreferredStep,vba.FinalStatus,\r\n            vba.ScreenerPersonId,vba.CertifierPersonId,\r\n            vba.CurrentProgressId AS ProgressStepId,wp.WorkflowGroupCode,wp.ProgressTitle,wp.ProgressDescription,wp.ProgressStepNumber,wp.ProgressStepTotalCount,\r\n            vba.DateCreated,vba.WhoCreatedPersonId,vba.DateLastModified,vba.WhoLastModifiedPersonId,\r\n            p.firstname,p.middlename,p.lastname,p.student_no,\r\n            vc.ChapterTitle,vc.ChapterDescription,vc.ChapterFormId,vc.IsDisabled,vc.OrderNum,\r\n            s.SemesterTitle,s.StartDate,s.EndDate\r\nFROM        VetsBenefitApplication vba LEFT JOIN people p ON p.personid=vba.PersonId\r\n            LEFT JOIN VetsChapter vc ON vc.ChapterId=vba.ChapterId\r\n            LEFT JOIN Semester s ON s.SemesterId=vba.SemesterId\r\n            LEFT JOIN WorkflowProgress wp ON wp.ProgressId=vba.CurrentProgressId\r\nWHERE vba.BenefitApplicationId=@id";

		// Token: 0x04000041 RID: 65
		internal const string QS_VETS_BENEFIT_APPLICATION_STATUS_BY_ID = "SELECT\tvba.BenefitApplicationId,\r\n        vba.FinalStatus,\r\n        vba.ScreenerPersonId AS personid,p.lastName,p.firstname,p.middlename,p.student_no,\r\n        vba.CertifierPersonId AS certifierpersonid,p2.lastName AS certifierlastname,p2.firstname AS certifierfirstname,p2.middlename AS certifiermiddlename,p2.student_no AS certifierstudent_no,\r\n\t\tvba.DateLastModified,\r\n        vba.CurrentProgressId AS ProgressStepId,wp.WorkflowGroupCode,wp.ProgressTitle,wp.ProgressDescription,wp.ProgressStepNumber,wp.ProgressStepTotalCount,\r\n\t\tvn.BenefitApplicationStatusDetailNotesId,vn.DateEntered,vn.ForStudent,vn.Note,\r\n\t\tvn.WhoEnteredPersonId,pwho.firstName AS WhoEnteredFirstName,pwho.middleName AS WhoEnteredMiddleName,\r\n\t\tpwho.lastName AS WhoEnteredLastName,pwho.student_no AS WhoEnteredStudent_no\r\nFROM\tVetsBenefitApplication vba LEFT JOIN people p ON p.PersonID=vba.ScreenerPersonId\r\n        LEFT JOIN people p2 ON p2.PersonID=vba.CertifierPersonId\r\n\t\tLEFT JOIN WorkflowProgress wp ON wp.ProgressId=vba.CurrentProgressId\r\n\t\tLEFT JOIN VetsBenefitApplicationStatusDetailNotes vn ON vn.BenefitApplicationId=vba.BenefitApplicationId\r\n\t\tLEFT JOIN people pwho ON pwho.PersonID=vn.WhoEnteredPersonId\r\nWHERE\tvba.BenefitApplicationId=@id\r\nORDER BY vn.DateEntered DESC";

		// Token: 0x04000042 RID: 66
		internal const string QU_BENEFIT_APPLICATION_STUDENT_INFO = "UPDATE VetsBenefitApplication SET \r\nregistrationcompleted=COALESCE(@registrationcompleted,registrationcompleted),\r\nchapterid=COALESCE(@chapterid,chapterid),\r\nbenappcompleted=COALESCE(@benappcompleted,benappcompleted),\r\nstudentagreecompleted=COALESCE(@studentagreecompleted,studentagreecompleted),\r\npreferredstep=COALESCE(@preferredstep,preferredstep),\r\ndatelastmodified=getdate()\r\nWHERE BenefitApplicationId=@id";

		// Token: 0x04000043 RID: 67
		internal const string QI_BENEFIT_APPLICATION = "DECLARE @newBenefitApplicationId [uniqueidentifier]\r\nIF NOT EXISTS(SELECT BenefitApplicationId FROM VetsBenefitApplication WHERE PersonId=@pid AND SemesterId=@semesterid)\r\nBEGIN\r\n\tINSERT INTO VetsBenefitApplication (BenefitApplicationId,PersonId,SemesterId,DateCreated,WhoCreatedPersonId)\r\n\tVALUES (@id,@pid,@semesterid,getdate(),@whoamipid)\r\n\tSET @newBenefitApplicationId=@id\r\nEND\r\n\r\nSET @newid=@newBenefitApplicationId";

		// Token: 0x04000044 RID: 68
		internal const string QI_BENEFIT_APPLICATION_MODIFICATION_ENTRY = "INSERT INTO VetsBenefitApplicationHistory (BenefitApplicationId,WhoEnteredPersonId,DateEntered,ModificationType) VALUES (@id,@whoamipid,getdate(),@modtype)\r\n\r\nUPDATE VetsBenefitApplication SET DateLastModified=getdate(),WhoLastModifiedPersonId=@whoamipid WHERE BenefitApplicationId=@id";
	}
}
