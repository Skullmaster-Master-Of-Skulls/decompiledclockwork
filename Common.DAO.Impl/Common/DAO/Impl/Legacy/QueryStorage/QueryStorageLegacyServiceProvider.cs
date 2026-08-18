using System;

namespace TechnoPro.Common.DAO.Impl.Legacy.QueryStorage
{
	// Token: 0x020000AE RID: 174
	public static class QueryStorageLegacyServiceProvider
	{
		// Token: 0x0400023F RID: 575
		internal const string QS_LOAD_REQUEST_NOTES_AND_SPECIALINSTRUCTIONS = "SELECT serviceproviderrequestid,notes,specialinstructions FROM serviceproviderrequests WHERE serviceproviderrequestid=@id";

		// Token: 0x04000240 RID: 576
		internal const string QU_UPDATE_REQUEST_NOTES_AND_SPECIALINSTRUCTIONS = "UPDATE serviceproviderrequests SET notes=@notes,specialinstructions=@si WHERE serviceproviderrequestid=@id";

		// Token: 0x04000241 RID: 577
		internal const string QU_UPDATE_REQUEST_DETAIL_NOTES = "UPDATE serviceproviderrequests SET notes=@notes WHERE serviceproviderrequestid=@id";

		// Token: 0x04000242 RID: 578
		internal const string QU_UPDATE_REQUEST = "UPDATE serviceproviderrequestdetail SET \r\n        counsellorpid=@counsellorpid,rationale=@rationale,specialrequest=@specialrequest,\r\n        [plan]=@plan,fsbswd=@fsbswd,fsOsapStatus=@fsOsapStatus,fsWSIB=@fsWSIB,\r\n        fsWSIBLetterOfApprovalFilename=@fsWSIBLetterOfApprovalFilename,fsWSIBLetterOfApprovalFile=@fsWSIBLetterOfApprovalFile,fsWSIBCaseWorkerPhone=@fsWSIBCaseWorkerPhone,fsFirstNations=@fsFirstNations,fsFirstNationsLetterOfApprovalFilename=@fsFirstNationsLetterOfApprovalFilename,fsFirstNationsLetterOfApprovalFile=@fsFirstNationsLetterOfApprovalFile,fsFirstNationsCaseWorkerPhone=@fsFirstNationsCaseWorkerPhone,fsInterpreterFund=@fsInterpreterFund,fsInterpreterFundCode=@fsInterpreterFundCode,fsOther=@fsOther,fsOtherDetail=@fsOtherDetail,\r\n        fsBSWDStatus=@fsBSWDStatus,\r\n        fsWSIBStatus=@fsWSIBStatus,\r\n        fsFirstNationsStatus =@fsFirstNationsStatus,\r\n        fsInterpreterFundStatus=@fsInterpreterFundStatus,\r\n        fsOtherStatus=@fsOtherStatus,\r\n        fsssd=@fsssd,\r\n        fsssdstatus=@fsssdstatus\r\nWHERE   serviceproviderrequestdetailid=@id";

		// Token: 0x04000243 RID: 579
		internal const string QU_UPDATE_PROVIDER = "UPDATE serviceproviders SET nickname=@nickname,registrationcomplete=@registrationcomplete,firstname=@firstname,middlename=@middlename,lastname=@lastname,student_no=@student_no,altid=@altid,additionalservices=@additionalservices,specialization=@specialization,notes1=@notes1,notes2=@notes2,email=@email,phone1=@phone1,phone2=@phone2,phonenote=@phonenote,address=@address,address2=@address2,email2=@email2,addressactive=@addressactive,address2active=@address2active WHERE serviceproviderid=@id";

		// Token: 0x04000244 RID: 580
		internal const string QI_CREATE_PROVIDER = "INSERT INTO serviceproviders (firstname,middlename,lastname,student_no,altid,additionalservices,specialization,notes1,notes2,email,phone1,phone2,phonenote,address,address2,email2,addressactive,address2active,nickname,registrationcomplete) VALUES (@firstname,@middlename,@lastname,@student_no,@altid,@additionalservices,@specialization,@notes1,@notes2,@email,@phone1,@phone2,@phonenote,@address,@address2,@email2,@addressactive,@address2active,@nickname,@registrationcomplete)\r\nSET @spid=(SELECT TOP 1 CAST(@@identity AS int) AS serviceproviderid FROM serviceproviders)";

		// Token: 0x04000245 RID: 581
		internal const string QS_LOAD_PROVIDER = "SELECT sp.serviceproviderid,sp.firstname,sp.middlename,sp.lastname,sp.student_no,sp.email\r\n        ,sp.phone1,sp.phone2,sp.specialization,sp.notes1,sp.notes2,sp.address\r\n        ,sp.address2,sp.email2,sp.addressactive,sp.address2active\r\n        ,spac.lucourseid,luc.startdate,luc.enddate,lucd.altlookupstring AS subject\r\n        ,luc.subjectid,luc.course,luc.timeofday,luc.section,tt.* \r\n        ,sp.altid,sp.nickname,sp.registrationcomplete\r\nFROM serviceproviders sp LEFT JOIN serviceproviderapplications spa ON spa.serviceproviderid=sp.serviceproviderid \r\n        LEFT JOIN serviceproviderapplicationcourses spac ON spac.serviceproviderapplicationid=spa.serviceproviderapplicationid \r\n        LEFT JOIN lucourses luc ON luc.lucourseid=spac.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN timetable tt ON tt.lucourseid=spac.lucourseid \r\nWHERE sp.serviceproviderid=@id";

		// Token: 0x04000246 RID: 582
		internal const string QS_PROVIDER_ID_BY_SNUM = "SELECT TOP 1 serviceproviderid FROM serviceproviders WHERE student_no=@sne";
	}
}
