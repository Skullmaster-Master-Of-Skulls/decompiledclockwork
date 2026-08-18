using System;

namespace ClockWorkAPI
{
	// Token: 0x0200006D RID: 109
	public class QueryStorage
	{
		// Token: 0x0400026F RID: 623
		public static readonly string QS_Select_LoadStudentCalendarForStudent = "SELECT app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid\r\n        ,at.description\r\n        --,at.iscourse\r\n         ,CASE WHEN ac.appointmentid IS NULL THEN CAST(0 AS bit) \r\nELSE CAST(1 as bit) \r\nEND AS iscourse\r\n        ,app.appcode,att.personid,att2.noshow\r\n        ,att2.personid AS personid2,p.firstname,p.lastname,ac.lucourseid\r\n        ,lucd.altlookupstring AS subject,luc.course,luc.section\r\n        ,app.subject AS subtitle,app.location \r\n        ,ac.originalstartdatetime,ac.originalenddatetime\r\n        ,pr.firstname AS room\r\nFROM appointments app LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid \r\n        LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\n        LEFT JOIN attendees att2 ON att2.appointmentid=app.appointmentid AND att2.personid IN (SELECT personid FROM peoplegroups WHERE groupid=2 OR personid IN (SELECT personid FROM peoplegroups WHERE groupid IN ( SELECT orderid AS groupid FROM splitorderids(@gids,','))) ) \r\n        LEFT JOIN people p ON p.personid=att2.personid \r\n        LEFT JOIN appointmentcourses ac ON ac.appointmentid=app.appointmentid \r\n        LEFT JOIN lucourses luc ON luc.lucourseid=ac.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN attendees attroom ON attroom.appointmentid=app.appointmentid AND attroom.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)\r\n        LEFT JOIN people pr ON pr.personid=attroom.personid\r\nWHERE att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) \r\n        AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 \r\n        AND (@apptypeids='' OR app.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,',')))\r\n        AND NOT (datepart(hour,app.startdate)=0 AND datepart(minute,app.startdate)=0 AND datepart(hour,app.enddate)=1 AND datepart(minute,app.enddate)=0)\r\nORDER BY app.startdate,app.appointmentid";

		// Token: 0x04000270 RID: 624
		public static readonly string QS_Select_LoadStudentCalendarForFacilitator = "SELECT app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid\r\n        ,at.description\r\n        --,at.iscourse\r\n         ,CASE WHEN ac.appointmentid IS NULL THEN CAST(0 AS bit) \r\nELSE CAST(1 as bit) \r\nEND AS iscourse\r\n        ,app.appcode,att.personid,att2.noshow\r\n        ,att2.personid AS personid2,p.firstname,p.lastname,ac.lucourseid\r\n        ,lucd.altlookupstring AS subject,luc.course,luc.section\r\n        ,app.subject AS subtitle,app.location \r\n        ,ac.originalstartdatetime,ac.originalenddatetime\r\n        ,pr.firstname AS room\r\nFROM appointments app LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid \r\n        LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\n        LEFT JOIN attendees att2 ON att2.appointmentid=app.appointmentid AND att2.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1 AND NOT personid IN (SELECT personid FROM peoplegroups WHERE groupid IN ( SELECT orderid AS groupid FROM splitorderids(@gids,','))) ) \r\n        LEFT JOIN people p ON p.personid=att2.personid \r\n        LEFT JOIN appointmentcourses ac ON ac.appointmentid=app.appointmentid \r\n        LEFT JOIN lucourses luc ON luc.lucourseid=ac.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN attendees attroom ON attroom.appointmentid=app.appointmentid AND attroom.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)\r\n        LEFT JOIN people pr ON pr.personid=attroom.personid\r\nWHERE att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) \r\n        AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 \r\n        AND (@apptypeids='' OR app.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,',')))\r\n        AND NOT (datepart(hour,app.startdate)=0 AND datepart(minute,app.startdate)=0 AND datepart(hour,app.enddate)=1 AND datepart(minute,app.enddate)=0)\r\nORDER BY app.startdate,app.appointmentid";

		// Token: 0x04000271 RID: 625
		public static readonly string QS_Select_LoadAllStudentsWritingTest2a = "SELECT    DISTINCT e.examid,a.appointmentid,e.dateoftest,e.testduration\r\n                ,a.personid,p.firstname,p.lastname,p.student_no\r\n                ,a.startdate,a.enddate,at.description\r\n                ,ac.InstructorAcknowledgeValue,ac.InstructorAcknowledgeDate\r\n    FROM    exams e LEFT JOIN apps a ON a.examid=e.examid\r\n            LEFT JOIN appointmenttypes at ON at.apptypeid=a.apptypeid\r\n            LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid\r\n            LEFT JOIN people p ON p.personid=a.personid\r\n    WHERE   e.examid=@examid AND a.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\n            AND a.cancelled=0";

		// Token: 0x04000272 RID: 626
		public static readonly string QS_Select_LoadAllStudentsWritingTest2b = "SELECT    DISTINCT e.examid,a.appointmentid,e.dateoftest,e.testduration\r\n                ,a.personid,p.firstname,p.lastname,p.student_no\r\n                ,a.startdate,a.enddate,at.description\r\n                ,0 AS InstructorAcknowledgeValue,NULL AS InstructorAcknowledgeDate\r\n    FROM    exams e LEFT JOIN apps a ON a.examid=e.examid\r\n            LEFT JOIN appointmenttypes at ON at.apptypeid=a.apptypeid\r\n            LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid\r\n            LEFT JOIN people p ON p.personid=a.personid\r\n    WHERE   e.examid=@examid AND a.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\n            AND a.cancelled=0";

		// Token: 0x04000273 RID: 627
		public static readonly string QS_Select_InstructorTestInfo = "SELECT description,dateoftest,testduration,lucourseid,visible,typecode FROM exams WHERE examid=@examid";

		// Token: 0x04000274 RID: 628
		public static readonly string QS_Select_StudentsTest = "SELECT ac.appointmentid FROM apps a LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid WHERE a.personid=@pid AND  CONVERT(DATETIME, FLOOR(CONVERT(FLOAT, a.startdate)))=CONVERT(DATETIME, FLOOR(CONVERT(FLOAT, @testdate ))) AND ac.lucourseid=@lucid";

		// Token: 0x04000275 RID: 629
		public static readonly string QS_Select_NumberOfBookedAppointments = "SELECT COUNT(appointmentid) FROM appointments WHERE startdate>getdate() AND cancelled=0 AND appointmentid IN (SELECT appointmentid FROM attendees WHERE personid=@pid) AND (@apptypeids='' OR apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,',')))";

		// Token: 0x04000276 RID: 630
		public static readonly string QS_Select_PeopleOnWaitingList = "SELECT DISTINCT personid FROM waitinglist WHERE appointmentid=@appid";

		// Token: 0x04000277 RID: 631
		public static readonly string QS_Select_Appointments = "SELECT DISTINCT app.appointmentid,app.startdate,app.enddate,app.apptypeid FROM attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid WHERE att.personid=@pid AND app.cancelled=0 AND app.startdate>=@sd";

		// Token: 0x04000278 RID: 632
		public static readonly string QS_Select_AppointmentByAppointmentId = "SELECT app.appointmentid,app.startdate,app.enddate,app.apptypeid,at.description,app.cancelled\r\nFROM appointments app LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid\r\nWHERE app.appointmentid=@appid";

		// Token: 0x04000279 RID: 633
		public static readonly string QS_Select_WaitingList = "SELECT wl.personid,wl.waitinglistid,wl.apptypeid FROM waitinglist wl LEFT JOIN appointments app ON app.appointmentid=wl.appointmentid WHERE wl.appointmentid=@appid AND app.cancelled=1 ORDER BY wl.waitinglistid";

		// Token: 0x0400027A RID: 634
		public static readonly string QS_Select_AppointmentTypes = "SELECT apptypeid,description FROM appointmenttypes WHERE apptypeid IN (SELECT orderid AS controlid FROM splitorderids(@ids,',')) ORDER BY description";

		// Token: 0x0400027B RID: 635
		public static readonly string QS_Select_TutorScheduleExistingApps = "SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\n            ,x.appointmentid AS currentuserappid\r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\n            LEFT JOIN (SELECT DISTINCT xatt.appointmentid FROM attendees xatt LEFT JOIN appointments xapp ON xapp.appointmentid=xatt.appointmentid WHERE xatt.personid=@pid AND xapp.cancelled=0) x ON x.appointmentid=app.appointmentid\r\nWHERE       att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate";

		// Token: 0x0400027C RID: 636
		public static readonly string QS_Select_RoomSchedules = "SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\nWHERE       att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate";

		// Token: 0x0400027D RID: 637
		public static readonly string QS_Select_StudentSchedule = "SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\nWHERE       att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate";

		// Token: 0x0400027E RID: 638
		public static readonly string QS_Select_StudentScheduleExceptAppointment = "SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\nWHERE       NOT app.appointmentid=@appid AND att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate";

		// Token: 0x0400027F RID: 639
		public static readonly string QS_Select_Availability = "SELECT a.personid,a.availabilitygroupid,a.availabilitydate,a.availability,-1 AS roomid FROM availabilityschedule a WHERE a.availabilitydate>=@sdate AND a.availabilitydate <=@edate AND a.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND a.availabilitygroupid IN (SELECT orderid AS availabilitygroupid FROM splitorderids( @agids, ',' ) ) ORDER BY a.personid,a.availabilitydate,a.availabilitygroupid";

		// Token: 0x04000280 RID: 640
		public static readonly string QS_Select_UserAppointmentsReverseOrder = "SELECT a.appointmentid,a.noshow FROM apps a WHERE a.personid=@pid AND a.startdate<=@beforedate ORDER BY a.startdate DESC";

		// Token: 0x04000281 RID: 641
		public static readonly string QS_Select_DynamicStringData = "SELECT o.controlvalue,p.firstname,p.lastname,p.student_no FROM otherinfops o LEFT JOIN people p ON p.personid=o.personid WHERE o.personid=@pid AND o.controlid=@cid";

		// Token: 0x04000282 RID: 642
		public static readonly string QS_Select_PreviouslySubmittedTests = "SELECT DISTINCT a.startdate,a.enddate FROM\r\n( SELECT    DISTINCT ac.originalstartdatetime AS startdate,\r\n            ac.originalenddatetime AS enddate \r\nFROM        appointmentcourses ac LEFT JOIN appointments app ON app.appointmentid=ac.appointmentid \r\nWHERE       ac.lucourseid=@lucid AND app.cancelled=0 \r\n            AND NOT ac.originalstartdatetime IS NULL \r\n            AND NOT ac.originalenddatetime IS NULL \r\n            AND ac.originalstartdatetime > @mindate\r\nUNION\r\nSELECT      DISTINCT e.dateentered AS startdate,\r\n            dateadd(n,e.testduration,e.dateentered) AS enddate\r\nFROM        exams e\r\nWHERE       e.lucourseid=@lucid \r\n            AND e.dateentered > @mindate\r\n) a\r\nORDER BY a.startdate";

		// Token: 0x04000283 RID: 643
		public static readonly string QS_SelectPreviouslySubmittedClassTestDefinitions = "SELECT DISTINCT e.dateoftest AS startdate,dateadd(n,e.testduration,e.dateoftest) AS enddate\r\nFROM    exams e \r\nWHERE   e.dateoftest>@mindate AND e.lucourseid=@lucid";

		// Token: 0x04000284 RID: 644
		public static readonly string QS_SelectPreviouslySubmittedRegistrarClassTestDefinitions = "SELECT DISTINCT e.dateoftest AS startdate,dateadd(n,e.testduration,e.dateoftest) AS enddate\r\nFROM    exams e \r\nWHERE   e.dateoftest>@mindate AND e.lucourseid=@lucid AND typecode='F'";

		// Token: 0x04000285 RID: 645
		public static readonly string QS_SelectPreviouslySubmittedClassTestDefinitionsTestsByTypeCode = "SELECT DISTINCT e.dateoftest AS startdate,dateadd(n,e.testduration,e.dateoftest) AS enddate\r\nFROM    exams e \r\nWHERE   e.dateoftest>@mindate AND e.lucourseid=@lucid \r\n        AND (@typecodesallowed='' OR typecode in (SELECT * FROM splitstrings(@typecodesallowed)))\r\n        AND (@typecodesnotallowed='' OR NOT typecode in (SELECT * FROM splitstrings(@typecodesnotallowed)))";

		// Token: 0x04000286 RID: 646
		public static readonly string QS_Select_ExternalStudentByUsername = "SELECT s.id,s.firstname,s.lastname,t.passwordhash\r\nFROM    testbooking_student s LEFT JOIN testbooking_student_external t ON t.studentid=s.id\r\nWHERE   s.email=@email";

		// Token: 0x04000287 RID: 647
		public static readonly string QS_Select_StudentTemplateAccommodations = "SELECT ad.* FROM accommodationdataactive ad WHERE ad.personid=@pid AND ad.courseid=0 AND ad.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";

		// Token: 0x04000288 RID: 648
		public static readonly string QS_Select_StudentAccommodationsFromAccommodationData = "DECLARE @usecourse int\r\nSELECT @usecourse = MAX(dataid) FROM accommodationdata WHERE personid=@pid AND courseid=@lucid\r\n\r\nSELECT    ad.personid,ad.courseid,ad.controlid,ad.controlcaption,dc.setting4string,ad.valtext,ad.valint,ad.valbytes,valbytesisencrypted \r\n            ,a.longdescription,a.showonletter\r\n  FROM      accommodationdata ad LEFT JOIN dynamiccontrols dc ON dc.controlid=ad.controlid\r\n            LEFT JOIN accommodations a ON a.controlid=ad.controlid\r\n  WHERE     personid=@pid AND\r\n           (  ( NOT @usecourse IS NULL AND courseid=@lucid )\r\n                OR\r\n               ( @usecourse IS NULL AND courseid=0 )\r\n            )\r\n            AND (ad.offline IS NULL OR ad.offline=0)\r\n            AND (ad.expirydate IS NULL OR ad.expirydate > getdate() )\r\n            AND (@showallaccommodations=1 OR a.showonletter>0)";

		// Token: 0x04000289 RID: 649
		public static readonly string QS_Select_StudentAccommodations = "SELECT\tDISTINCT a.dataid,a.personid,a.controlid,a.controlcode,a.controlcaption\r\n\t\t,a.valtext,a.valint,a.valdate,NULL AS valimage\r\n\t\t,a.altlongdescription \r\n\t\t,a.valbytes,a.valbytesisencrypted,a.setting1,a.setting2,a.setting3,a.setting4\r\n\t\t,acc.longDescription,dc.setting4string,sca.lucourseid2\r\nFROM\tstudentcourseaccommodations sca LEFT JOIN accommodationdataactive a ON a.PersonID=sca.personid AND a.courseid=sca.lucourseid2 \r\n\t\tLEFT JOIN Accommodations acc ON acc.ControlID=a.ControlID\r\n\t\tLEFT JOIN DynamicControls dc ON dc.ControlID=a.ControlID \r\nWHERE\tsca.PersonID=@personid\r\n\t\tAND sca.lucourseid2=@lucourseid \r\n\t\tAND (acc.showonletter & 2) = 2";

		// Token: 0x0400028A RID: 650
		public static readonly string QS_Select_DynamicDataOtherInfoPS = "SELECT controlvalue FROM otherinfops WHERE personid=@pid AND controlid=@cid";

		// Token: 0x0400028B RID: 651
		public static readonly string QS_Select_DateTimePsData = "SELECT dataid FROM datetimeinfops WHERE personid=@pid AND controlid=@cid AND controlvalue>=@d";

		// Token: 0x0400028C RID: 652
		public static readonly string QS_Select_TutorsWithBios = "SELECT p.personid,p.firstname,p.lastname,p.student_no,o.controlvalue AS info FROM people p LEFT JOIN otherinfops o ON o.personid=p.personid AND o.controlid=@cid WHERE p.personid IN (SELECT personid FROM peoplegroups WHERE groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,',')))";

		// Token: 0x0400028D RID: 653
		public static readonly string QS_Select_DynamicControls = "SELECT DISTINCT dsc.controlid,@screennum AS screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue\r\n    ,dc.ControlName,dc.ControlGroup,dc.HelpText,dc.HelpTextDisplayMethod,dc.Mask,dc.Enforce,dc.ActionHandlers,dc.DefaultValueString,dc.Setting4String,dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,dc.fontsize,dc.dontwraptonextline\r\n    ,s.description,dsc.ordernum\r\nFROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum\r\nWHERE (@screennum<=0 OR dsc.screennum=@screennum) AND dsc.isactive=@true \r\n     AND dc.enabled=@true \r\n     AND (@exemptcids='' OR NOT dsc.controlid IN (SELECT orderid AS controlid FROM splitorderids(@exemptcids,','))) \r\nORDER BY dsc.ordernum";

		// Token: 0x0400028E RID: 654
		public static readonly string QS_Select_LookupListEnglishWithFirstBlankItem = "SELECT NULL AS lookuplistid,NULL AS lookupgroupid,NULL AS lookuptext,-999 AS ordernum,'' AS children UNION SELECT lookuplistid,lookupgroupid,lookuptext,ordernum,children FROM lookuplists WHERE lookupgroupid=@lookupgroupid ORDER BY ordernum,lookuptext";

		// Token: 0x0400028F RID: 655
		public static readonly string QS_Select_LookupListFrenchWithFirstBlankItem = "SELECT NULL AS lookuplistid,NULL AS lookupgroupid,NULL AS lookuptext,-999 AS ordernum,'' AS children UNION SELECT lookuplistid,lookupgroupid,coalesce(nullif(lookupvalue,''),lookuptext) AS lookuptext,ordernum,children FROM lookuplists WHERE lookupgroupid=@lookupgroupid ORDER BY ordernum,lookuptext";

		// Token: 0x04000290 RID: 656
		public static readonly string QS_Select_LookupListEnglishNoFirstBlankItem = "SELECT lookuplistid,lookupgroupid,lookuptext,ordernum,children FROM lookuplists WHERE lookupgroupid=@lookupgroupid ORDER BY ordernum,lookuptext";

		// Token: 0x04000291 RID: 657
		public static readonly string QS_Select_LookupListFrenchNoFirstBlankItem = "SELECT lookuplistid,lookupgroupid,coalesce(nullif(lookupvalue,''),lookuptext) AS lookuptext,ordernum,children FROM lookuplists WHERE lookupgroupid=@lookupgroupid ORDER BY ordernum,lookuptext";

		// Token: 0x04000292 RID: 658
		public static readonly string QS_Select_LookupListChildren = "SELECT childlist FROM lookupgroups WHERE lookupgroupid=@lookupgroupid";

		// Token: 0x04000293 RID: 659
		public static readonly string QS_Select_PSData = "SELECT    pd.personid,pd.controlid,pd.controlcaption,pd.valtext,pd.valbytesisencrypted\r\n    ,pd.valint,pd.valbytes,pd.valdate\r\n    ,pd.controlcaption,dc.setting4string\r\n    ,pd.setting1,pd.setting2,pd.setting3,pd.setting4,pd.defaultvalue,pd.controlcode\r\nFROM        perstudentdata2 pd LEFT JOIN dynamiccontrols dc ON dc.controlid=pd.controlid \r\nWHERE       pd.personid=@pid \r\n            AND pd.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))\r\n";

		// Token: 0x04000294 RID: 660
		public static readonly string QS_Select_StudentNotes2 = "SELECT TOP @numsamplenotes nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated,nd.notes,nd.lecturedate,sp.firstname,sp.lastname,sp.student_no,nd.issamplenotes,nd.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.subjectid,luc.course,luc.timeofday,luc.section,lucd2.altlookupstring AS instructor,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS CourseDescription,nd.docname AS description,nd.issamplenotes \r\nFROM    notetakerdocument nd LEFT JOIN serviceproviders sp ON sp.serviceproviderid=nd.notetakerid \r\n        LEFT JOIN lucourses luc ON luc.lucourseid=nd.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE nd.notetakerid=@spid \r\n        AND nd.lucourseid=@lucidsp \r\n        --AND nd.issamplenotes=1 \r\n        AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) ) \r\nORDER BY nd.lecturedate";

		// Token: 0x04000295 RID: 661
		public static readonly string QS_Select_StudentNotes = "SELECT DISTINCT x.* FROM\r\n(\r\n    SELECT  DISTINCT nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated\r\n                ,nd.notetakerid,nd.lucourseid,nd.notes,nd.lecturedate \r\n    FROM\tNotetakerDocument nd \r\n    WHERE\tnd.NotetakerID=@spid\r\n\t\t    AND nd.LUCourseId=@splucid -- IN (SELECT serviceproviderlucourseid FROM serviceproviderrequests WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid AND serviceprovidertype=128)\r\n\t\t    AND (@includesamplenotes=1 OR nd.issamplenotes=1)\r\n    /*\r\n    UNION\r\n    SELECT  DISTINCT nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated\r\n                ,nd.notetakerid,nd.lucourseid,nd.notes,nd.lecturedate \r\n    FROM        serviceproviderrequestshistory spr LEFT JOIN notetakerdocument nd ON nd.notetakerid=spr.serviceproviderid\r\n                                                    AND nd.lucourseid=spr.serviceproviderlucourseid\r\n    WHERE   spr.personid=@pid \r\n            AND spr.serviceprovidertype=128 \r\n            AND spr.lucourseid=@lucid\r\n            AND spr.stillactive=1\r\n            AND (@includesamplenotes=1 OR nd.issamplenotes=1) AND NOT spr.serviceproviderid IS NULL \r\n            AND NOT nd.notetakerdocumentid IS NULL\r\n    */\r\n) x\r\nORDER BY x.lecturedate DESC";

		// Token: 0x04000296 RID: 662
		public static readonly string QS_Select_NotetakerStudentsCourses = "SELECT c.lucourseid,luc.term,luc.duration,luc.startdate,luc.enddate\r\n        ,luc.subjectid,lucd.altlookupstring AS subject,luc.course,luc.timeofday\r\n        ,luc.section,spr.serviceproviderid,spr.serviceproviderlucourseid\r\nFROM courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\n        LEFT JOIN serviceproviderrequests spr ON spr.personid=@pid AND spr.lucourseid=@lucid\r\nWHERE   c.personid=@pid AND c.lucourseid=@lucid \r\n        AND NOT spr.serviceproviderid IS NULL";

		// Token: 0x04000297 RID: 663
		public static readonly string QS_Select_ServiceProviderApplicationId = "SELECT spac.serviceproviderapplicationid FROM serviceproviderapplicationcourses spac WHERE spac.lucourseid IN (SELECT lucourseid FROM equivalentcourses1(@lucid))";

		// Token: 0x04000298 RID: 664
		public static readonly string QS_Select_StudentsCourses = "SELECT c.lucourseid,luc.term,luc.duration,luc.startdate,luc.enddate\r\n        ,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section\r\n        ,lucd2.altlookupstring AS instructorname,spr.studentrequested AS notetakerrequired\r\n        ,spr.serviceproviderrequestid,spr.serviceproviderid\r\n        ,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS CourseDescription \r\n        ,COUNT(sph.serviceproviderrequesthistoryid) AS NumHistory\r\nFROM courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\n        LEFT JOIN serviceproviderrequests spr ON spr.personid=@pid AND spr.lucourseid=c.lucourseid AND spr.serviceprovidertype=128 \r\n        LEFT JOIN serviceproviderrequestshistory sph ON sph.serviceproviderrequestid=spr.serviceproviderrequestid\r\nWHERE c.personid=@pid AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) )\r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\nGROUP BY c.lucourseid,luc.term,luc.duration,luc.startdate,luc.enddate\r\n        ,lucd.altlookupstring,luc.course,luc.timeofday,luc.section\r\n        ,lucd2.altlookupstring,spr.studentrequested \r\n        ,spr.serviceproviderrequestid,spr.serviceproviderid\r\n        ,lucd.altlookupstring,luc.course,luc.timeofday,luc.section";

		// Token: 0x04000299 RID: 665
		public static readonly string QS_Select_PotentialNotetakers = "SELECT    spac.lucourseid,spa.serviceproviderid,sp.firstname,sp.lastname,sp.student_no\r\n                ,spr.serviceproviderrequestid,spr.personid\r\n                ,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS CourseDescription\r\n                ,CAST(0 AS INT) AS activenotetakerothercourse\r\n  FROM      ServiceProviderApplicationCourses spac LEFT JOIN serviceproviderapplications spa ON spa.serviceproviderapplicationid=spac.serviceproviderapplicationid\r\n            LEFT JOIN serviceproviders sp ON sp.serviceproviderid=spa.serviceproviderid\r\n            LEFT JOIN serviceproviderrequests spr ON spr.lucourseid=spac.lucourseid AND spr.serviceproviderid=spa.serviceproviderid AND spr.serviceprovidertype=128\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=spac.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n  WHERE     spac.lucourseid IN (SELECT lucourseid FROM equivalentcourses(@lucid))\r\n            AND spa.serviceprovidertype=128 \r\n  ORDER BY spa.serviceproviderid,spac.lucourseid";

		// Token: 0x0400029A RID: 666
		public static readonly string QS_Select_ServiceProviderById = "SELECT student_no,firstname,lastname,email FROM serviceproviders WHERE serviceproviderid=@id";

		// Token: 0x0400029B RID: 667
		public static readonly string QS_Select_ServiceProviderByStudent_no2 = "SELECT student_no,firstname,lastname,email,serviceproviderid FROM serviceproviders WHERE student_no=@snume";

		// Token: 0x0400029C RID: 668
		public static readonly string QS_Select_Notes2 = "SELECT DISTINCT TOP 3 nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated,nd.notes,nd.lecturedate,sp.firstname,sp.lastname,sp.student_no,nd.issamplenotes,nd.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.subjectid,luc.course,luc.timeofday,luc.section,lucd2.altlookupstring AS instructor,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS CourseDescription,nd.docname AS description \r\nFROM    notetakerdocument nd LEFT JOIN serviceproviders sp ON sp.serviceproviderid=nd.notetakerid \r\n        LEFT JOIN lucourses luc ON luc.lucourseid=nd.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE nd.notetakerid=@spid \r\n        AND nd.lucourseid=@lucid2 \r\n        --AND nd.issamplenotes=1 \r\n        AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) ) \r\nORDER BY nd.lecturedate";

		// Token: 0x0400029D RID: 669
		public static readonly string QS_Select_NotetakerCourses = "SELECT    sp.serviceproviderid,sp.firstname,sp.lastname,sp.student_no,sp.email\r\n,spac.lucourseid,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section\r\n,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS CourseDescription\r\n,sr.serviceproviderrequestid\r\n,luc.term,luc.duration,luc.startdate,luc.enddate\r\n FROM       serviceproviders sp LEFT JOIN serviceproviderapplications spa ON spa.serviceproviderid=sp.serviceproviderid AND spa.serviceprovidertype=128\r\n            LEFT JOIN serviceproviderapplicationcourses spac ON spac.serviceproviderapplicationid=spa.serviceproviderapplicationid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=spac.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN serviceproviderrequests sr ON sr.serviceproviderlucourseid=spac.lucourseid AND sr.serviceproviderid=sp.serviceproviderid\r\n WHERE      sp.serviceproviderid=@id \r\n            AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) )\r\n ORDER BY   spac.lucourseid";

		// Token: 0x0400029E RID: 670
		public static readonly string QS_Select_ServiceProviderInfo = "SELECT    sp.serviceproviderid,sp.firstname,sp.lastname,sp.middlename,sp.student_no\r\n            ,sp.email,sp.address,sp.phone1,sp.phone2 \r\n            ,sp.address2,sp.addressactive,sp.address2active,sp.email2\r\nFROM serviceproviders sp WHERE sp.serviceproviderid=@nid";

		// Token: 0x0400029F RID: 671
		public static readonly string QS_Select_Notes = "SELECT nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated,nd.notetakerid,nd.lucourseid,nd.notes,nd.lecturedate FROM notetakerdocument nd WHERE nd.lucourseid=@lucid AND nd.notetakerid=@spid";

		// Token: 0x040002A0 RID: 672
		public static readonly string QS_Select_NotetakerForCourse = "SELECT serviceproviderrequestid FROM serviceproviderrequests WHERE isactive=1 AND serviceproviderid=@spid AND serviceproviderlucourseid=@lucid";

		// Token: 0x040002A1 RID: 673
		public static readonly string QS_Select_ANotetakersStudents = "SELECT sr.serviceproviderrequestid,p.firstname,p.lastname,p.student_no,oi.controlvalue AS email FROM serviceproviderrequests sr LEFT JOIN people p ON p.personid=sr.personid LEFT JOIN otherinfops oi ON oi.personid=sr.personid AND oi.controlid=@emailcid WHERE sr.isactive=1 AND sr.serviceproviderid=@id AND lucourseid IN (SELECT DISTINCT luc2.lucourseid FROM lucourses luc LEFT JOIN lucourses luc2 ON luc2.subjectid=luc.subjectid AND luc2.course=luc.course WHERE luc.lucourseid=@lucid)";

		// Token: 0x040002A2 RID: 674
		public static readonly string QS_Select_NumberProvidingFor = "SELECT COUNT(*) FROM serviceproviderrequests sr WHERE sr.isactive=1 AND sr.serviceproviderid=@id AND lucourseid IN (SELECT DISTINCT luc2.lucourseid FROM lucourses luc LEFT JOIN lucourses luc2 ON luc2.subjectid=luc.subjectid AND luc2.course=luc.course WHERE luc.lucourseid=@lucid)";

		// Token: 0x040002A3 RID: 675
		public static readonly string QS_Select_ServiceProviderByEmail = "SELECT sp.serviceproviderid FROM serviceproviders sp WHERE sp.email=@emailbytes AND isactive=1 ORDER BY serviceproviderid DESC";

		// Token: 0x040002A4 RID: 676
		public static readonly string QS_Select_ServiceProviderByStudent_no = "SELECT serviceproviderid FROM serviceproviders WHERE isactive=1 AND student_no=@sne";

		// Token: 0x040002A5 RID: 677
		public static readonly string QS_Select_InstructorByEmail = "SELECT lucoursedataid AS instructorid FROM lucoursedata WHERE lookuplisttype=1 AND email=@email";

		// Token: 0x040002A6 RID: 678
		public static readonly string QS_Select_InstructorByEmail2 = "SELECT ui.instructoremail,ui.password,a.altlookupstring,a.phone,a.lucoursedataid FROM userinstructor ui LEFT JOIN lucoursedata a ON a.lookuplisttype=1 AND a.email=ui.instructoremail WHERE ui.instructoremail=@email";

		// Token: 0x040002A7 RID: 679
		public static readonly string QS_Select_InstructorByUsername = "SELECT lucoursedataid AS instructorid FROM lucoursedata WHERE username=@username";

		// Token: 0x040002A8 RID: 680
		public static readonly string QS_Select_InstructorByEmailOrUsername = "SELECT lucoursedataid AS instructorid FROM lucoursedata WHERE lookuplisttype=1 AND (email=@email OR username=@username)";

		// Token: 0x040002A9 RID: 681
		public static readonly string QS_Select_AltContactByEmailOrUsername = "SELECT alternatecontactid FROM lucoursealternatecontact WHERE isactive=1 AND (altemail=@email OR altusername=@username)";

		// Token: 0x040002AA RID: 682
		public static readonly string QS_Login_User = "SELECT ui.personid,ui.pass,p.student_no,pg.groupid FROM people p LEFT JOIN userinfo ui ON ui.personid=p.personid LEFT JOIN peoplegroups pg ON pg.personid=p.personid WHERE p.isactive=1 AND ui.username=@uname AND p.personid IN (SELECT personid FROM peoplegroups WHERE groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,','))) ORDER BY p.personid,pg.groupid";

		// Token: 0x040002AB RID: 683
		public static readonly string QS_Select_UserByStudent_no = "SELECT p.personid,p.firstname,p.lastname FROM people p WHERE p.isactive=1 AND p.student_no=@snume";

		// Token: 0x040002AC RID: 684
		public static readonly string QS_Select_UserByDynamicDataEncryptedString = "SELECT oi.personid,oi.controlvalue FROM otherinfops oi WHERE oi.controlid=@cid AND (oi.controlvalue=@be OR oi.controlvalue=@b) AND oi.personid IN (SELECT personid FROM people WHERE isactive=1)";

		// Token: 0x040002AD RID: 685
		public static readonly string QS_Select_StudentInfo = "SELECT p.personid,p.firstname,p.lastname,p.middlename,p.student_no,oi.controlvalue AS email FROM people p LEFT JOIN otherinfops oi ON oi.personid=p.personid AND oi.controlid=@cid WHERE p.personid=@pid";

		// Token: 0x040002AE RID: 686
		public static readonly string QS_Select_StudentInfo2 = "DECLARE @cid int\r\nSET @cid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=260)\r\n\r\nSELECT p.personid,p.firstname,p.lastname,p.middlename,p.student_no,oi.valtext,oi.valbytes,oi.valbytesisencrypted\r\nFROM people p LEFT JOIN perstudentdata2 oi ON oi.personid=p.personid AND oi.controlid=@cid \r\nWHERE p.personid=@pid";

		// Token: 0x040002AF RID: 687
		public static readonly string QS_Select_GroupMembership = "SELECT personid FROM peoplegroups WHERE personid=@pid AND groupid=@gid";

		// Token: 0x040002B0 RID: 688
		public static readonly string QS_Select_Password = "SELECT pass FROM userinfo WHERE username=@username AND personid IN (SELECT personid FROM people WHERE isactive=1)";

		// Token: 0x040002B1 RID: 689
		public static readonly string QS_Select_StaffPassword = "SELECT pass FROM userinfo WHERE username=@username AND personid IN (SELECT personid FROM people WHERE isactive=1) AND personid IN (SELECT personid FROM peoplegroups WHERE groupid=2)";

		// Token: 0x040002B2 RID: 690
		public static readonly string QS_Select_UserByPersonId = "SELECT p.personid,p.firstname,p.lastname,p.student_no FROM people p WHERE p.personid=@pid";

		// Token: 0x040002B3 RID: 691
		public static readonly string QS_InsertUpdate_CourseAlternateContact = "DECLARE @id int\r\nIF EXISTS(SELECT alternatecontactid FROM lucoursealternatecontact WHERE altemail=@email)\r\nBEGIN\r\n    SET @id = (SELECT alternatecontactid FROM lucoursealternatecontact WHERE altemail=@email)\r\n    UPDATE lucoursealternatecontact SET altname=@name WHERE LEN(@name)>0 AND alternatecontactid=@id;\r\n    UPDATE lucoursealternatecontact SET altphone=@phone WHERE LEN(@phone)>0 AND alternatecontactid=@id;\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucoursealternatecontact(altname,altemail,altphone,altusername,altpermissionlevel,whocreated,datecreated,isactive) \r\n        VALUES (@name,@email,@phone,'',0,-555,getdate(),1);\r\n    SET @id=(SELECT MAX(CAST(SCOPE_IDENTITY() AS int)) FROM lucoursealternatecontact)\r\nEND\r\n\r\nUPDATE lucourses SET alternatecontactid=@id WHERE lucourseid=@lucid";

		// Token: 0x040002B4 RID: 692
		public static readonly string QS_Select_CourseTimetableByCourse = "SELECT t.*\r\nFROM t.timetable t\r\nWHERE t.timetabletype='C' AND t.lucourseid=@lucid";

		// Token: 0x040002B5 RID: 693
		public static readonly string QS_Select_CourseTimetableByStudent = "SELECT t.*\r\nFROM timetable t\r\nWHERE t.timetabletype='C' AND t.lucourseid IN (SELECT lucourseid FROM courses WHERE personid=@pid AND NOT lucourseid=@lucid AND (registrationstatus IS NULL OR NOT registrationstatus=2))";

		// Token: 0x040002B6 RID: 694
		public static readonly string QS_Select_CourseTimetableByStudent2 = "SELECT  luc.StartDate,luc.EndDate,t.*\r\nFROM Courses c LEFT JOIN LUCourses luc ON luc.LUCourseID=c.luCourseID \r\n\t\tLEFT JOIN timetable t ON t.lucourseid=c.luCourseID \r\nWHERE\tc.personID=@pid \r\n\t\tAND NOT c.lucourseid=@lucid \r\n\t\tAND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\n\t\tAND NOT ( ( luc.enddate<@startdate ) OR (luc.startdate > @enddate ) )\r\n\t\tAND NOT t.timetableid IS NULL";

		// Token: 0x040002B7 RID: 695
		public static readonly string QS_Select_CourseByInstructor = "SELECT DISTINCT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\n            luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\n            lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\n            lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session, \r\n            lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS coursedescription \r\nFROM lucourses luc \r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tluc.instructorid=@iid \r\n        AND NOT ( ( luc.enddate<@startdate) OR (luc.startdate > @enddate ) )\r\n        AND luc.lucourseid IN (SELECT lucourseid FROM courses WHERE registrationstatus IS NULL OR NOT registrationstatus=2)\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040002B8 RID: 696
		public static readonly string QS_Select_CourseByAltContact = "SELECT DISTINCT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\n            luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\n            lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\n            lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session, \r\n            lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS coursedescription \r\n            ,ac.altpermissionlevel\r\nFROM lucourses luc LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tNOT ( ( luc.enddate<@startdate) OR (luc.startdate > @enddate ) )\r\n        AND luc.alternatecontactid=@altcontactid\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040002B9 RID: 697
		public static readonly string QS_Select_CourseByAltContact_ExcludeCoursesWhereNoClassTestDefinitionExists = "SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\n            luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\n            lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\n            lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session, \r\n            lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS coursedescription \r\n            ,ac.altpermissionlevel\r\nFROM lucourses luc LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tNOT ( ( luc.enddate<@startdate) OR (luc.startdate > @enddate ) )\r\n        AND luc.alternatecontactid=@altcontactid\r\n        AND luc.lucourseid IN (SELECT lucourseid FROM exams)\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040002BA RID: 698
		public static readonly string QS_Select_CourseByInstructor_ExcludeCoursesWhereNoClassTestDefinitionExists = "SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\n            luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\n            lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\n            lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session, \r\n            lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS coursedescription \r\nFROM lucourses luc \r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tluc.instructorid=@iid \r\n        AND NOT ( ( luc.enddate<@startdate) OR (luc.startdate > @enddate ) )\r\n        AND luc.lucourseid IN (SELECT lucourseid FROM exams)\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040002BB RID: 699
		public static readonly string QS_Select_FindSubjectId = "SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=0 AND altlookupstring=@subject";

		// Token: 0x040002BC RID: 700
		public static readonly string QS_Select_FindCourseBySubjectCourseSection = "SELECT lucourseid FROM lucourses WHERE subjectid IN (SELECT lucoursedataid AS subjectid FROM lucoursedata WHERE lookuplisttype=0 AND altlookupstring=@subject) AND course=@course AND section=@section AND NOT ( ( enddate<@sdate ) OR (startdate > @edate ) )";

		// Token: 0x040002BD RID: 701
		public static readonly string QS_SelectStudentCourse2 = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\n    luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\n    luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\n    lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\n    lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\n    LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n    LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tc.personid=@personid AND c.lucourseid=@lucid AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040002BE RID: 702
		public static readonly string QS_Select_StudentCourse = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\nlucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tc.personid=@personid AND luc.enddate >= @startdate AND c.lucourseid=@lucid AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040002BF RID: 703
		public static readonly string QS_Select_StudentCourseMultiple = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\nlucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tc.personid=@personid\r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\n        AND c.lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040002C0 RID: 704
		public static readonly string QS_Select_StudentCourses = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\nlucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tc.personid=@personid AND luc.enddate >= @startdate \r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040002C1 RID: 705
		public static readonly string QS_Select_StudentCoursesCurrentTerm = "DECLARE @sdate datetime\r\nDECLARE @edate datetime\r\nDECLARE @now datetime\r\nSET @now = getdate()\r\nDECLARE @m int\r\nSET @m = MONTH(@now)\r\n\r\nif @m>=9 \r\nbegin\r\n   SET @sdate = cast(year(@now) AS char(4)) + '-09-01'\r\n   SET @edate = cast(year(@now) AS char(4)) + '-12-31'\r\nend\r\nelse if @m>=5\r\nbegin\r\n   SET @sdate = cast((year(@now)) AS char(4)) + '-05-01'\r\n   SET @edate = cast((year(@now)) AS char(4)) + '-08-30'\r\nend\r\nelse \r\nbegin\r\n   SET @sdate = cast(year(@now) AS char(4)) + '-01-01'\r\n   SET @edate = cast((year(@now)) AS char(4)) + '-04-30'\r\nend\r\n\r\nSELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\nlucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tc.personid=@pid \r\n\t\tAND (NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) ))\r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\n        AND (@campuses='' OR luc.campus IS NULL OR luc.campus='' OR (NOT luc.campus IS NULL AND luc.campus IN (SELECT orderid AS campus FROM splitstrings(@campuses))))\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040002C2 RID: 706
		public static readonly string QS_Select_StudentCoursesRestrictByCampus = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\nlucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tc.personid=@personid AND luc.enddate >= @startdate \r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\n        AND (@campuses='' OR luc.campus IS NULL OR luc.campus='' OR (NOT luc.campus IS NULL AND luc.campus IN (SELECT orderid AS campus FROM splitstrings(@campuses))))\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040002C3 RID: 707
		public static readonly string QS_Select_StudentCoursesAndFinalExamDates = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\nlucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\n,e.dateoftest,e.testduration\r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nLEFT JOIN exams e ON e.lucourseid=c.lucourseid AND e.dateoftest>=@startdate AND e.dateoftest<=@enddate AND e.typecode='F'\r\nWHERE\tc.personid=@personid AND luc.enddate >= @startdate AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040002C4 RID: 708
		public static readonly string QS_Select_StudentCourse2 = "SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.course,luc.section,luc.timeofday,lucd2.altlookupstring AS instructorname,lucd2.email AS instructoremail,lucd2.phone AS instructorphone FROM lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid WHERE luc.lucourseid=@lucid";

		// Token: 0x040002C5 RID: 709
		public static readonly string QS_Select_CourseInfo = "SELECT    luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term\r\n            ,luc.instructorid\r\n            ,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section\r\n            ,lucd2.altlookupstring AS instructor,lucd2.lookupstring AS instructor2\r\n            ,lucd2.phone AS instructorphone\r\n            ,lucd2.email AS instructoremail,lucd2.username\r\n            ,lucd.email AS subjectemail\r\nFROM        lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\nWHERE       luc.lucourseid=@lucid";

		// Token: 0x040002C6 RID: 710
		public static readonly string QS_Select_Template = "SELECT emisc,ebodypdf FROM emailtemplates WHERE templateid=@templateid";

		// Token: 0x040002C7 RID: 711
		public static readonly string QS_Select_EveryoneSetting = "SELECT settingstringvalue,settingvalue FROM settingsgroups WHERE settingcode=@settingcode";

		// Token: 0x040002C8 RID: 712
		public static readonly string QS_Update_SelectANotetaker = "UPDATE serviceproviderrequests SET serviceproviderid=@spid,dateassigned=getdate(),serviceproviderlucourseid=@splucid WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid";

		// Token: 0x040002C9 RID: 713
		public static readonly string QS_Update_ApplicationCourseDateCancelled = "UPDATE serviceproviderapplicationcourses SET datecancelled=null WHERE serviceproviderapplicationcourseid=@id";

		// Token: 0x040002CA RID: 714
		public static readonly string QS_Update_MarkAllBookingsAsNonTentative = "UPDATE appointments SET appcode=0\r\n    WHERE appointmentid IN (SELECT ac.appointmentid FROM exams e LEFT JOIN appointmentcourses ac \r\n                                    ON DATEADD(dd, DATEDIFF(dd,0,e.dateoftest), 0)=DATEADD(dd, DATEDIFF(dd,0,ac.originalstartdatetime), 0)\r\n\t\t\t\t\t\t\t\t\t\tAND ac.LUCourseID=e.lucourseid \r\n                            WHERE e.examid=@examid)";

		// Token: 0x040002CB RID: 715
		public static readonly string QS_Update_ExamInfo = "UPDATE exams SET \r\n    lucourseid=@lucid,lastmodified=getdate(),description=@description,dateoftest=@dateoftest,wholastmodified=@iid,testduration=@testduration,instructoracknowledged=@instructoracknowledged\r\n    WHERE examid=@examid";

		// Token: 0x040002CC RID: 716
		public static readonly string QS_Update_WaitingListEntry = "UPDATE waitinglist SET skippedreason=@reason WHERE waitinglistid=@id";

		// Token: 0x040002CD RID: 717
		public static readonly string QS_UPDATE_UpdateInstructorPassword = "UPDATE userinstructor SET password=@pwd WHERE instructoremail=@email";

		// Token: 0x040002CE RID: 718
		public static readonly string QS_UPDATE_UpdateStudentNumber = "UPDATE people SET student_no=@sne WHERE personid=@pid";

		// Token: 0x040002CF RID: 719
		public static readonly string QS_UPDATE_UpdateDynamicDataPSDateTime1 = "UPDATE datetimeinfops SET controlvalue=@cv WHERE personid=@pid AND controlid=@pid";

		// Token: 0x040002D0 RID: 720
		public static readonly string QS_INSERT_UpdateDynamicDataPSDateTime2 = "INSERT INTO datetimeinfops (screennum,controlid,controlvalue,personid) SELECT @0,@cid,@cv,@pid WHERE NOT EXISTS(SELECT dataid FROM datetimeinfops WHERE personid=@pid AND controlid=@cid)";

		// Token: 0x040002D1 RID: 721
		public static readonly string QS_UPDATE_UpdateDynamicDataPSOtherInfo1 = "UPDATE otherinfops SET controlvalue=@cv WHERE personid=@pid AND controlid=@pid";

		// Token: 0x040002D2 RID: 722
		public static readonly string QS_INSERT_UpdateDynamicDataPSOtherInfo2 = "INSERT INTO otherinfops (screennum,controlid,controlvalue,personid) SELECT @0,@cid,@cv,@pid WHERE NOT EXISTS(SELECT dataid FROM otherinfops WHERE personid=@pid AND controlid=@cid)";

		// Token: 0x040002D3 RID: 723
		public static readonly string QS_INSERTUPDATE_InsertOrUpdateDynamicDataPSOther = "IF EXISTS(SELECT dataid FROM otherinfops WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE otherinfops SET controlvalue=@cv WHERE personid=@pid AND controlid=@cid\r\n    SELECT dataid FROM otherinfops WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfops (screennum,controlid,controlvalue,personid) SELECT 0,@cid,@cv,@pid WHERE NOT EXISTS(SELECT dataid FROM otherinfops WHERE personid=@pid AND controlid=@cid);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int);\r\nEND";

		// Token: 0x040002D4 RID: 724
		public static readonly string QS_UPDATE_UnassignNotetaker1 = "INSERT INTO serviceproviderrequestshistory (ServiceProviderRequestID,personid,lucourseid,datetimerequesttitle,startdatetimerequest,enddatetimerequest,serviceprovidertype,dateentered,startdate,enddate,whoentered,ServiceProviderId,ServiceProviderRequestDetailId,notes,studentrequested,studentrequestedcancelnote,DateAssigned,SpecialInstructions,partsgroupid,partsdescription,serviceproviderlucourseid,stillactive) SELECT ServiceProviderRequestID,personid,lucourseid,datetimerequesttitle,startdatetimerequest,enddatetimerequest,serviceprovidertype,dateentered,startdate,enddate,whoentered,ServiceProviderId,ServiceProviderRequestDetailId,notes,studentrequested,studentrequestedcancelnote,DateAssigned,SpecialInstructions,partsgroupid,partsdescription,serviceproviderlucourseid,@stillactive FROM serviceproviderrequests WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid AND serviceprovidertype=128";

		// Token: 0x040002D5 RID: 725
		public static readonly string QS_UPDATE_UnassignNotetaker2 = "UPDATE serviceproviderrequests SET dateassigned=NULL,serviceproviderid=NULL,studentrequested=0,studentrequestedcancelnote=@note,serviceproviderlucourseid=NULL WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid AND serviceprovidertype=128";

		// Token: 0x040002D6 RID: 726
		public static readonly string QS_UPDATE_MarkStudentAsOfficiallyRequested = "IF NOT EXISTS(SELECT serviceproviderrequestid FROM serviceproviderrequests WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid)\r\nBEGIN\r\n    DECLARE @sdate DATETIME\r\n    DECLARE @edate DATETIME\r\n    if MONTH(getdate())>=9 \r\n    BEGIN\r\n        SET @sdate=cast(cast(year(GETDATE()) AS char(4)) + '-09-01' AS datetime) \r\n        SET @edate=cast(cast((year(GETDATE())+1) AS char(4)) + '-04-30' AS datetime)\r\n    END\r\n    else if MONTH(getdate())>=5\r\n    BEGIN\r\n        SET @sdate=cast(cast((year(GETDATE())) AS char(4)) + '-05-01' AS datetime) \r\n        SET @edate=CAST(cast((year(GETDATE())) AS char(4)) + '-08-30' AS datetime)\r\n    END\r\n    else\r\n    BEGIN \r\n        SET @sdate=cast(cast((year(GETDATE())-1) AS char(4)) + '-09-01' AS datetime) \r\n        SET @edate=CAST(cast((year(GETDATE())) AS char(4)) + '-04-30' AS datetime)\r\n    END\r\n\r\n    DECLARE @detailid int\r\n    IF EXISTS(SELECT serviceproviderrequestdetailid FROM serviceproviderrequests WHERE isactive=1 AND personid=@pid AND startdate>=@sdate AND startdate<=@edate)\r\n        SET @detailid = (SELECT TOP 1 serviceproviderrequestdetailid FROM serviceproviderrequests WHERE isactive=1 AND personid=@pid AND startdate>=@sdate AND startdate<=@edate)\r\n    ELSE\r\n    BEGIN\r\n\t\tINSERT INTO serviceproviderrequestdetail (counsellorpid) VALUES (@pid);\r\n\t\tSET @detailid=(SELECT CAST(SCOPE_IDENTITY() AS int))\r\n\tEND\r\n\r\n    INSERT INTO serviceproviderrequests (serviceproviderrequestdetailid,personid,lucourseid,serviceprovidertype,startdate,enddate,whoentered,studentrequested) VALUES (@detailid,@pid,@lucid,128,@sdate,@edate,@pid,1)\r\nEND\r\nELSE\r\nBEGIN \r\n    UPDATE serviceproviderrequests SET studentrequested=1 WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid\r\nEND";

		// Token: 0x040002D7 RID: 727
		public static readonly string QS_UPDATE_UpdateServiceProviderCancelDate = "UPDATE serviceproviderapplicationcourses SET datecancelled=getdate(),note=@note WHERE lucourseid=@lucid AND serviceproviderapplicationid IN (SELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@id)";

		// Token: 0x040002D8 RID: 728
		public static readonly string QS_UPDATE_RemoveServiceProviderFromAllRequests = "UPDATE serviceproviderrequests SET serviceproviderid=NULL,dateassigned=NULL\r\n            WHERE isactive=1 AND serviceproviderid=@id AND serviceproviderlucourseid=@lucid AND serviceprovidertype=@sptype";

		// Token: 0x040002D9 RID: 729
		public static readonly string QS_UPDATE_RemoveServiceProviderAvailability = "DELETE FROM serviceproviderapplicationcourses \r\n            WHERE lucourseid=@lucid AND serviceprovidertype=@sptype AND serviceproviderapplicationid IN (SELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@id)";

		// Token: 0x040002DA RID: 730
		public static readonly string QS_INSERT_ActivateStudent = "INSERT INTO peoplepreviousyears (personid,dateactive) VALUES (@pid,@now)";

		// Token: 0x040002DB RID: 731
		public static readonly string QS_INSERT_PersonRow = "INSERT INTO people (student_no,firstname,middlename,lastname,dateadded,isactive) VALUES (@sne,@fne,@mne,@lne,@now,@true); SELECT CAST(@@IDENTITY AS int);";

		// Token: 0x040002DC RID: 732
		public static readonly string QS_INSERT_AddUserToGroup = "INSERT INTO peoplegroups (personid,groupid,isprimarygroup) VALUES (@pid,@gid,@primarygroup)";

		// Token: 0x040002DD RID: 733
		public static readonly string QS_INSERT_NewServiceProviderApplicationCourse = "IF EXISTS(SELECT serviceproviderapplicationcourseid FROM serviceproviderapplicationcourses WHERE serviceproviderapplicationid=@spa AND serviceprovidertype=@sptype AND lucourseid=@lucid)\r\nBEGIN\r\n    SELECT serviceproviderapplicationcourseid FROM serviceproviderapplicationcourses WHERE serviceproviderapplicationid=@spa AND serviceprovidertype=@sptype AND lucourseid=@lucid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO serviceproviderapplicationcourses \r\n            (serviceproviderapplicationid,serviceprovidertype,lucourseid,datecancelled) \r\n    VALUES  (@spa,@sptype,@lucid,NULL);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS serviceproviderapplicationcourseid\r\nEND";

		// Token: 0x040002DE RID: 734
		public static readonly string QS_INSERT_NewServiceProvider = "IF LEN(@altid)>0\r\nBEGIN\r\nINSERT INTO serviceproviders \r\n    (altid,firstname,middlename,lastname,student_no,email,phone1,phone2,phonenote,address,address2,addressactive,address2active,email2) \r\nSELECT @altid,@firstname,@middlename,@lastname,@student_no,@email,@phone1,@phone2,@phonenote,@address,@address2,@addressactive,@address2active,@email2\r\n    WHERE NOT EXISTS(SELECT serviceproviderid FROM serviceproviders WHERE altid=@altid AND isactive=1); SELECT serviceproviderid FROM serviceproviders WHERE altid=@altid AND isactive=1\r\nEND\r\nELSE\r\nBEGIN\r\nINSERT INTO serviceproviders \r\n    (firstname,middlename,lastname,student_no,email,phone1,phone2,phonenote,address,address2,addressactive,address2active,email2) \r\nSELECT @firstname,@middlename,@lastname,@student_no,@email,@phone1,@phone2,@phonenote,@address,@address2,@addressactive,@address2active,@email2\r\n    WHERE NOT EXISTS(SELECT serviceproviderid FROM serviceproviders WHERE student_no=@student_no AND isactive=1); SELECT serviceproviderid FROM serviceproviders WHERE student_no=@student_no AND isactive=1\r\nEND";

		// Token: 0x040002DF RID: 735
		public static readonly string QS_UPDATE_ServiceProvider = "UPDATE serviceproviders SET \r\n    firstname=@firstname,middlename=@middlename,lastname=@lastname,student_no=@student_no\r\n    ,email=@email,phone1=@phone1,phone2=@phone2,address=@address,address2=@address2 \r\n    ,addressactive=@addressactive,address2active=@address2active,email2=@email2\r\nWHERE serviceproviderid=@nid";

		// Token: 0x040002E0 RID: 736
		public static readonly string QS_INSERT_NewServiceProviderApplication = "IF EXISTS(SELECT serviceproviderid FROM serviceproviderapplications WHERE serviceproviderid=@spid AND serviceprovidertype=@sptype)\r\n\tSELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@spid AND serviceprovidertype=@sptype\r\n\tELSE \r\n\tBEGIN\r\nINSERT INTO serviceproviderapplications (serviceproviderid,serviceprovidertype,note1,note2,dateentered,whoentered,ispermanent,isactive,isactivecomment) \r\nSELECT @spid,@sptype,'','',getdate(),1,NULL,1,NULL  \r\nSELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderapplicationid=@@identity\r\nEND";

		// Token: 0x040002E1 RID: 737
		public static readonly string QS_INSERT_NewServiceProviderApplicationInTerm = "IF EXISTS(SELECT serviceproviderid FROM serviceproviderapplications WHERE serviceproviderid=@spid AND serviceprovidertype=@sptype AND (dateentered BETWEEN @termstartdate AND @termenddate) )\r\n\tSELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@spid AND serviceprovidertype=@sptype AND (dateentered BETWEEN @startdate AND @enddate)\r\n\tELSE \r\n\tBEGIN\r\nDECLARE @newdate DATETIME\r\nIF getdate() BETWEEN @termstartdate AND @termenddate\r\n    SET @newdate=getdate()\r\nELSE\r\n    SET @newdate=DATEADD(day,-1,@termenddate)\r\n\r\nINSERT INTO serviceproviderapplications (serviceproviderid,serviceprovidertype,note1,note2,dateentered,whoentered,ispermanent,isactive,isactivecomment) \r\nSELECT @spid,@sptype,'','',@newdate,1,NULL,1,NULL  \r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS serviceproviderapplicationid\r\nEND";

		// Token: 0x040002E2 RID: 738
		public static readonly string QS_INSERT_Notes = "INSERT INTO NotetakerDocument (docName, numPages, sizeInBytes, dateCreated, binaryData, NoteTakerId, LUCourseId, notes, lectureDate, issamplenotes) VALUES(@docName, @numPages, @sizeInBytes, @dateCreated, @binaryData, @NoteTakerID, @LUCourseId, @notes, @lectureDate, @issamplenotes)";

		// Token: 0x040002E3 RID: 739
		public static readonly string QS_INSERT_AddToWaitingList = "INSERT INTO waitinglist (personid,appointmentid,dateadded,whoadded,apptypeid) VALUES (@pid,@appid,getdate(),@pid,@apptypeid); SELECT waitinglistid FROM waitinglist WHERE waitinglistid=@@identity";

		// Token: 0x040002E4 RID: 740
		public static readonly string QS_INSERT_NewAttendee = "INSERT INTO attendees (personid,appointmentid,noshow,misccode) VALUES (@pid,@appid,0,-1)";

		// Token: 0x040002E5 RID: 741
		public static readonly string QS_INSERT_NewTestDefinition = "INSERT INTO exams (dateentered,whoentered,lucourseid,description,dateoftest,visible,usercomment,testduration,lastmodified,wholastmodified)\r\nSELECT getdate(),@pid,@lucid,'',@dateoftest,1,'',@testduration,getdate(),@pid\r\nWHERE NOT EXISTS (SELECT examid FROM exams WHERE lucourseid=@lucid AND dateoftest>=@sd AND dateoftest<=@ed)";

		// Token: 0x040002E6 RID: 742
		public static readonly string QS_INSERT_CreateTest = "INSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,appcode,groupcode,examid) \r\nSELECT @apptypeid,@startdate,@enddate,@cancelled,@dateadded,@personid,@ishidden,@islocked,@extraattendeescount,@appcode,@groupcode,@examid\r\nWHERE (@dontcareifroombooked=1 OR NOT EXISTS (SELECT att.personid FROM attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid WHERE att.personid=@rid AND app.cancelled=0 AND app.startdate>=@sdmn AND app.startdate<@edmn))\r\nAND NOT EXISTS (SELECT att.personid FROM attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid LEFT JOIN appointmentcourses ac ON ac.appointmentid=att.appointmentid WHERE att.personid=@pid AND app.cancelled=0 AND ac.lucourseid=@lucid AND app.startdate>=@sdmn AND app.startdate<@edmn) \r\nAND NOT EXISTS (SELECT appointmentid FROM apps WHERE PersonID=@pid AND cancelled=0 AND NOT ( ( enddate<@startdate ) OR (startdate > @enddate ) ) )\r\n; SELECT @@IDENTITY;";

		// Token: 0x040002E7 RID: 743
		public static readonly string QS_INSERT_CreateTest2 = "DECLARE @sd0 datetime, @sd1 datetime\r\nSET @sd0 = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nSET @sd1 = DATEADD(day,1,@sd0)\r\n\r\nIF EXISTS(SELECT a.appointmentid FROM apps a WHERE a.PersonID=@pid AND a.cancelled=0 AND NOT ( ( a.enddate<=@startdate ) OR (a.startdate >= @enddate ) ))\r\n\tSELECT 0 AS appid,'studentbooked' AS failedreason\r\nELSE IF @dontcareifroombooked=0 AND EXISTS(SELECT a.appointmentid FROM apps a WHERE @dontcareifroombooked=0 AND @rid>0 AND a.PersonID=@rid AND a.cancelled=0 AND NOT ( ( a.enddate<=@startdate ) OR (a.startdate >= @enddate ) ))\r\n\tSELECT 0 AS appid,'roombooked' AS failedreason\r\nELSE IF EXISTS( SELECT ac.appointmentid FROM apps a LEFT JOIN AppointmentCourses ac ON ac.AppointmentID=a.AppointmentID WHERE a.personid=@pid AND a.cancelled=0 AND a.startDate >=@sd0 AND a.startDate<@sd1 AND NOT ac.AppointmentID IS NULL AND ac.LUCourseID=@lucid )\r\n\tSELECT 0 AS appid,'alreadybookedsamecoursesameday' AS failedreason\r\nELSE\r\nBEGIN\r\n\tINSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,appcode,groupcode,examid) \r\n\t\tSELECT @apptypeid,@startdate,@enddate,0,getdate(),@pid,0,0,0,@appcode,-1,@examid\r\n\t\t\r\n\tSELECT CAST(@@IDENTITY AS int) AS appid,'' AS failedreason\r\nEND";

		// Token: 0x040002E8 RID: 744
		public static readonly string QS_INSERT_CreateTest3 = "DECLARE @sd0 datetime, @sd1 datetime\r\nSET @sd0 = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nSET @sd1 = DATEADD(day,1,@sd0)\r\n\r\nIF @ignoreapps=0 AND EXISTS(SELECT a.appointmentid FROM apps a WHERE a.PersonID=@pid AND a.cancelled=0 AND NOT ( ( a.enddate<=@startdate ) OR (a.startdate >= @enddate ) ))\r\n\tSELECT 0 AS appid,'studentbooked' AS failedreason\r\nELSE IF @dontcareifroombooked=0 AND EXISTS(SELECT a.appointmentid FROM apps a WHERE @dontcareifroombooked=0 AND @rid>0 AND a.PersonID=@rid AND a.cancelled=0 AND NOT ( ( a.enddate<=@startdate ) OR (a.startdate >= @enddate ) ))\r\n\tSELECT 0 AS appid,'roombooked' AS failedreason\r\nELSE IF EXISTS( SELECT ac.appointmentid FROM apps a LEFT JOIN AppointmentCourses ac ON ac.AppointmentID=a.AppointmentID WHERE a.personid=@pid AND a.cancelled=0 AND a.startDate >=@sd0 AND a.startDate<@sd1 AND NOT ac.AppointmentID IS NULL AND ac.LUCourseID=@lucid )\r\n\tSELECT 0 AS appid,'alreadybookedsamecoursesameday' AS failedreason\r\nELSE\r\nBEGIN\r\n\tINSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,appcode,groupcode,examid,totalbreakminutes) \r\n\t\tSELECT @apptypeid,@startdate,@enddate,0,getdate(),@pid,0,0,0,@appcode,-1,@examid,@totalbreakminutes\r\n\t\t\r\n\tSELECT CAST(@@IDENTITY AS int) AS appid,'' AS failedreason\r\nEND";

		// Token: 0x040002E9 RID: 745
		public static readonly string QS_INSERTUPDATE_GetExam = "IF NOT EXISTS(SELECT examid FROM exams WHERE lucourseid=@lucid AND dateoftest>=@sdate AND dateoftest<@edate)\r\nBEGIN\r\n    INSERT INTO exams (dateentered,whoentered,lucourseid,description,dateoftest,testduration,lastmodified,wholastmodified,visible,usercomment,typecode) VALUES (getdate(),-555,@lucid,'',@dateoftest,@testduration,getdate(),NULL,1,NULL,'N')\r\nEND\r\nSELECT examid FROM exams WHERE lucourseid=@lucid AND dateoftest>=@sdate AND dateoftest<@edate";

		// Token: 0x040002EA RID: 746
		public static readonly string QS_Insert_Attendee = "INSERT INTO attendees (appointmentid,personid,noshow,misccode) VALUES (@appid,@pid,@noshow,@misccode)";

		// Token: 0x040002EB RID: 747
		public static readonly string QS_Insert_AppointmentCourses = "INSERT INTO appointmentcourses (appointmentid,lucourseid,originalstartdatetime,originalenddatetime,testnote,studentnote) VALUES (@appointmentid,@lucid,@classsd,@classed,@testnote,@studentnote)";

		// Token: 0x040002EC RID: 748
		public static readonly string QS_INSERT_AddCourse = "INSERT INTO lucourses (startdate,enddate,term,duration,subjectid,course,timeofday,section,instructorid,crosslistcode,equivalentcode,coursenote,whoadded,dateadded,location) VALUES (@startdate,@enddate,@term,@duration,@subjectid,@course,@timeofday,@section,@iid,-1,-1,'',@nid,getdate(),''); SELECT lucourseid FROM lucourses WHERE lucourseid=@@identity";

		// Token: 0x040002ED RID: 749
		public static readonly string QS_INSERT_AddSubject = "INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone) VALUES (0,@subject,@subject,'',''); SELECT lucoursedataid FROM lucoursedata WHERE lucoursedataid=@@identity";

		// Token: 0x040002EE RID: 750
		public static readonly string QS_Delete_WaitingListEntry = "DELETE FROM waitinglist WHERE waitinglistid=@id";

		// Token: 0x040002EF RID: 751
		public static readonly string QS_Delete_DeleteAttendee = "DELETE FROM attendees WHERE appointmentid=@appid AND personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)";

		// Token: 0x040002F0 RID: 752
		public static readonly string QS_DELETE_Note = "DELETE FROM notetakerdocument WHERE notetakerdocumentid=@id";
	}
}
