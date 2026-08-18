using System;

namespace ClockWorkWebAPI
{
	// Token: 0x02000025 RID: 37
	public class QueryStorage
	{
		// Token: 0x040000A3 RID: 163
		public static readonly string QS_Select_LoadStudentCalendarForFacilitator = "SELECT app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid\r\n        ,at.description\r\n        --,at.iscourse\r\n         ,CASE WHEN ac.appointmentid IS NULL THEN CAST(0 AS bit) \r\nELSE CAST(1 as bit) \r\nEND AS iscourse\r\n        ,app.appcode,att.personid,att2.noshow\r\n        ,att2.personid AS personid2,p.firstname,p.lastname,ac.lucourseid\r\n        ,lucd.altlookupstring AS subject,luc.course,luc.section\r\n        ,app.subject AS subtitle,app.location \r\n        ,ac.originalstartdatetime,ac.originalenddatetime\r\n        ,pr.firstname AS room\r\nFROM appointments app LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid \r\n        LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\n        LEFT JOIN attendees att2 ON att2.appointmentid=app.appointmentid AND att2.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1 AND NOT personid IN (SELECT personid FROM peoplegroups WHERE groupid IN ( SELECT orderid AS groupid FROM splitorderids(@gids,','))) ) \r\n        LEFT JOIN people p ON p.personid=att2.personid \r\n        LEFT JOIN appointmentcourses ac ON ac.appointmentid=app.appointmentid \r\n        LEFT JOIN lucourses luc ON luc.lucourseid=ac.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN attendees attroom ON attroom.appointmentid=app.appointmentid AND attroom.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)\r\n        LEFT JOIN people pr ON pr.personid=attroom.personid\r\n        LEFT JOIN examstatuslookup esl ON esl.ExamStatusLookupId=ac.ExamStatusLookupId \r\nWHERE att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) \r\n        AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 \r\n        AND (@apptypeids='' OR app.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,',')))\r\n        AND NOT (datepart(hour,app.startdate)=0 AND datepart(minute,app.startdate)=0 AND datepart(hour,app.enddate)=1 AND datepart(minute,app.enddate)=0)\r\n        AND (esl.HideFromStudent IS NULL OR esl.HideFromStudent=0)\r\nORDER BY app.startdate,app.appointmentid";

		// Token: 0x040000A4 RID: 164
		public static readonly string QS_Select_LoadAllStudentsWritingTest2a = "SELECT    DISTINCT e.examid,a.appointmentid,e.dateoftest,e.testduration\r\n                ,a.personid,p.firstname,p.lastname,p.student_no\r\n                ,a.startdate,a.enddate,at.description\r\n                ,ac.InstructorAcknowledgeValue,ac.InstructorAcknowledgeDate,\r\n                c.coursesid\r\n    FROM    exams e LEFT JOIN apps a ON a.examid=e.examid\r\n            LEFT JOIN appointmenttypes at ON at.apptypeid=a.apptypeid\r\n            LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid\r\n            LEFT JOIN people p ON p.personid=a.personid\r\n            LEFT JOIN courses c ON c.personid=a.personid AND c.lucourseid=e.lucourseid\r\n    WHERE   e.examid=@examid AND a.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\n            AND a.cancelled=0\r\n            AND NOT c.coursesid IS NULL AND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)";

		// Token: 0x040000A5 RID: 165
		public static readonly string QS_Select_LoadAllStudentsWritingTest2b = "SELECT    DISTINCT e.examid,a.appointmentid,e.dateoftest,e.testduration\r\n                ,a.personid,p.firstname,p.lastname,p.student_no\r\n                ,a.startdate,a.enddate,at.description\r\n                ,0 AS InstructorAcknowledgeValue,NULL AS InstructorAcknowledgeDate,\r\n                c.coursesid\r\n    FROM    exams e LEFT JOIN apps a ON a.examid=e.examid\r\n            LEFT JOIN appointmenttypes at ON at.apptypeid=a.apptypeid\r\n            LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid\r\n            LEFT JOIN people p ON p.personid=a.personid\r\n            LEFT JOIN courses c ON c.personid=a.personid AND c.lucourseid=e.lucourseid\r\n    WHERE   e.examid=@examid AND a.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\n            AND a.cancelled=0\r\n            AND NOT c.coursesid IS NULL AND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)";

		// Token: 0x040000A6 RID: 166
		public static readonly string QS_Select_InstructorTestInfo = "SELECT description,dateoftest,testduration,lucourseid,visible,typecode FROM exams WHERE examid=@examid";

		// Token: 0x040000A7 RID: 167
		public static readonly string QS_Select_StudentsTest = "SELECT ac.appointmentid FROM apps a LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid WHERE a.personid=@pid AND  CONVERT(DATETIME, FLOOR(CONVERT(FLOAT, a.startdate)))=CONVERT(DATETIME, FLOOR(CONVERT(FLOAT, @testdate ))) AND ac.lucourseid=@lucid";

		// Token: 0x040000A8 RID: 168
		public static readonly string QS_Select_NumberOfBookedAppointments = "SELECT COUNT(appointmentid) FROM appointments WHERE startdate>getdate() AND cancelled=0 AND appointmentid IN (SELECT appointmentid FROM attendees WHERE personid=@pid) AND (@apptypeids='' OR apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,',')))";

		// Token: 0x040000A9 RID: 169
		public static readonly string QS_Select_PeopleOnWaitingList = "SELECT DISTINCT personid FROM waitinglist WHERE appointmentid=@appid";

		// Token: 0x040000AA RID: 170
		public static readonly string QS_Select_AllAppointments_By_Pid = "SELECT DISTINCT app.appointmentid,app.startdate,app.enddate,app.apptypeid,att.noshow,app.cancelled\r\nFROM attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid \r\nWHERE att.personid=@pid ORDER BY startdate DESC";

		// Token: 0x040000AB RID: 171
		public static readonly string QS_Select_AppointmentByAppointmentId = "SELECT app.appointmentid,app.startdate,app.enddate,app.apptypeid,at.description,app.cancelled\r\nFROM appointments app LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid\r\nWHERE app.appointmentid=@appid";

		// Token: 0x040000AC RID: 172
		public static readonly string QS_Select_WaitingList = "SELECT wl.personid,wl.waitinglistid,wl.apptypeid FROM waitinglist wl LEFT JOIN appointments app ON app.appointmentid=wl.appointmentid WHERE wl.appointmentid=@appid AND app.cancelled=1 ORDER BY wl.waitinglistid";

		// Token: 0x040000AD RID: 173
		public static readonly string QS_Select_AppointmentTypes = "SELECT apptypeid,description FROM appointmenttypes WHERE apptypeid IN (SELECT orderid AS controlid FROM splitorderids(@ids,',')) ORDER BY description";

		// Token: 0x040000AE RID: 174
		public static readonly string QS_Select_TutorScheduleExistingApps = "SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\n            ,x.appointmentid AS currentuserappid\r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\n            LEFT JOIN (SELECT DISTINCT xatt.appointmentid FROM attendees xatt LEFT JOIN appointments xapp ON xapp.appointmentid=xatt.appointmentid WHERE xatt.personid=@pid AND xapp.cancelled=0) x ON x.appointmentid=app.appointmentid\r\nWHERE       att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate";

		// Token: 0x040000AF RID: 175
		public static readonly string QS_Select_RoomSchedules = "SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\nWHERE       att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate";

		// Token: 0x040000B0 RID: 176
		public static readonly string QS_Select_StudentSchedule = "SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\nWHERE       att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate";

		// Token: 0x040000B1 RID: 177
		public static readonly string QS_Select_StudentScheduleExceptAppointment = "SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\nWHERE       NOT app.appointmentid=@appid AND att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate";

		// Token: 0x040000B2 RID: 178
		public static readonly string QS_Select_Availability = "SELECT a.personid,a.availabilitygroupid,a.availabilitydate,a.availability,-1 AS roomid FROM availabilityschedule a WHERE a.availabilitydate>=@sdate AND a.availabilitydate <=@edate AND a.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND a.availabilitygroupid IN (SELECT orderid AS availabilitygroupid FROM splitorderids( @agids, ',' ) ) ORDER BY a.personid,a.availabilitydate,a.availabilitygroupid";

		// Token: 0x040000B3 RID: 179
		public static readonly string QS_Select_UserAppointmentsReverseOrder = "SELECT a.appointmentid,a.noshow FROM apps a WHERE a.personid=@pid AND a.startdate<=@beforedate ORDER BY a.startdate DESC";

		// Token: 0x040000B4 RID: 180
		public static readonly string QS_Select_DynamicStringData = "SELECT o.controlvalue,p.firstname,p.lastname,p.student_no FROM otherinfops o LEFT JOIN people p ON p.personid=o.personid WHERE o.personid=@pid AND o.controlid=@cid";

		// Token: 0x040000B5 RID: 181
		public static readonly string QS_Select_PreviouslySubmittedTests = "SELECT DISTINCT a.startdate,a.enddate FROM\r\n( SELECT    DISTINCT ac.originalstartdatetime AS startdate,\r\n            ac.originalenddatetime AS enddate \r\nFROM        appointmentcourses ac LEFT JOIN appointments app ON app.appointmentid=ac.appointmentid \r\nWHERE       ac.lucourseid=@lucid AND app.cancelled=0 \r\n            AND NOT ac.originalstartdatetime IS NULL \r\n            AND NOT ac.originalenddatetime IS NULL \r\n            AND ac.originalstartdatetime >= @mindate\r\nUNION\r\nSELECT      DISTINCT e.dateentered AS startdate,\r\n            dateadd(n,e.testduration,e.dateentered) AS enddate\r\nFROM        exams e\r\nWHERE       e.lucourseid=@lucid \r\n            AND e.dateentered >= @mindate\r\n) a\r\nORDER BY a.startdate";

		// Token: 0x040000B6 RID: 182
		public static readonly string QS_SelectPreviouslySubmittedClassTestDefinitions = "SELECT DISTINCT e.dateoftest AS startdate,dateadd(n,e.testduration,e.dateoftest) AS enddate\r\nFROM    exams e \r\nWHERE   e.dateoftest>=@mindate AND e.lucourseid=@lucid";

		// Token: 0x040000B7 RID: 183
		public static readonly string QS_SelectPreviouslySubmittedRegistrarClassTestDefinitions = "SELECT DISTINCT e.dateoftest AS startdate,dateadd(n,e.testduration,e.dateoftest) AS enddate\r\nFROM    exams e \r\nWHERE   e.dateoftest>=@mindate AND e.lucourseid=@lucid AND typecode='F'";

		// Token: 0x040000B8 RID: 184
		public static readonly string QS_SelectPreviouslySubmittedClassTestDefinitionsTestsByTypeCode = "SELECT DISTINCT e.dateoftest AS startdate,dateadd(n,e.testduration,e.dateoftest) AS enddate\r\nFROM    exams e \r\nWHERE   e.dateoftest>=@mindate AND e.lucourseid=@lucid \r\n        AND (@typecodesallowed='' OR typecode in (SELECT * FROM splitstrings(@typecodesallowed)))\r\n        AND (@typecodesnotallowed='' OR NOT typecode in (SELECT * FROM splitstrings(@typecodesnotallowed)))";

		// Token: 0x040000B9 RID: 185
		public static readonly string QS_Select_ExternalStudentByUsername = "SELECT s.id,s.firstname,s.lastname,t.passwordhash\r\nFROM    testbooking_student s LEFT JOIN testbooking_student_external t ON t.studentid=s.id\r\nWHERE   s.email=@email";

		// Token: 0x040000BA RID: 186
		public static readonly string QS_Select_StudentTemplateAccommodations = "SELECT ad.* FROM accommodationdataactive ad WHERE ad.personid=@pid AND ad.courseid=0 AND ad.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";

		// Token: 0x040000BB RID: 187
		public static readonly string QS_Select_StudentAccommodations = "DECLARE @lucid int = (SELECT CASE WHEN EXISTS(SELECT dataid FROM accommodationdata WHERE personid=@personid AND courseid=@lucourseid) THEN @lucourseid ELSE 0 END AS lucid)\r\n\r\nSELECT\tDISTINCT a.dataid,a.personid,a.controlid,a.controlcode,a.controlcaption\r\n\t\t,a.valtext,a.valint,a.valdate,NULL AS valimage\r\n\t\t,a.altlongdescription \r\n\t\t,a.valbytes,a.valbytesisencrypted,a.setting1,a.setting2,a.setting3,a.setting4\r\n\t\t,acc.longDescription,dc.setting4string,sca.lucourseid2\r\nFROM\tstudentcourseaccommodations sca LEFT JOIN accommodationdataactive a ON a.PersonID=sca.personid AND a.courseid=sca.lucourseid2 \r\n\t\tLEFT JOIN Accommodations acc ON acc.ControlID=a.ControlID\r\n\t\tLEFT JOIN DynamicControls dc ON dc.ControlID=a.ControlID \r\nWHERE\tsca.PersonID=@personid\r\n\t\tAND sca.lucourseid2=@lucid\r\n\t\tAND (acc.showonletter & 2) = 2\r\n        AND a.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=4)";

		// Token: 0x040000BC RID: 188
		public static readonly string QS_Select_DynamicDataOtherInfoPS = "SELECT controlvalue FROM otherinfops WHERE personid=@pid AND controlid=@cid";

		// Token: 0x040000BD RID: 189
		public static readonly string QS_Select_DateTimePsData = "SELECT dataid FROM datetimeinfops WHERE personid=@pid AND controlid=@cid AND controlvalue>=@d";

		// Token: 0x040000BE RID: 190
		public static readonly string QS_Select_TutorsWithBios = "SELECT p.personid,p.firstname,p.lastname,p.student_no,o.controlvalue AS info FROM people p LEFT JOIN otherinfops o ON o.personid=p.personid AND o.controlid=@cid WHERE p.personid IN (SELECT personid FROM peoplegroups WHERE groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,',')))";

		// Token: 0x040000BF RID: 191
		public static readonly string QS_Select_DynamicControls = "SELECT DISTINCT dsc.controlid,@screennum AS screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue\r\n    ,dc.ControlName,COALESCE(dsc.ControlGroup,dc.ControlGroup) AS ControlGroup,dc.HelpText,dc.HelpTextDisplayMethod,dc.Mask,dc.Enforce,dc.ActionHandlers,dc.DefaultValueString,dc.Setting4String,dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,dc.fontsize,dc.dontwraptonextline\r\n    ,s.description,dsc.ordernum\r\nFROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum\r\nWHERE (@screennum>0 AND dsc.screennum=@screennum) AND dsc.isactive=@true \r\n     AND dc.enabled=@true \r\n     AND (@exemptcids='' OR NOT dsc.controlid IN (SELECT orderid AS controlid FROM splitorderids(@exemptcids,','))) \r\nORDER BY dsc.ordernum";

		// Token: 0x040000C0 RID: 192
		public static readonly string QS_Select_DynamicControls_ExemptByControlName = "SELECT DISTINCT dsc.controlid,@screennum AS screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue\r\n    ,dc.ControlName,COALESCE(dsc.ControlGroup,dc.ControlGroup) AS ControlGroup,dc.HelpText,dc.HelpTextDisplayMethod,dc.Mask,dc.Enforce,dc.ActionHandlers,dc.DefaultValueString,dc.Setting4String,dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,dc.fontsize,dc.dontwraptonextline\r\n    ,s.description,dsc.ordernum\r\nFROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum\r\nWHERE (@screennum>0 AND dsc.screennum=@screennum) AND dsc.isactive=1 \r\n     AND dc.enabled=1 \r\n     AND (@exemptnames='' OR dc.controlname IS NULL OR dc.controlname='' OR NOT dc.controlname IN (SELECT orderid AS controlid FROM splitstrings2(@exemptnames,','))) \r\nORDER BY dsc.ordernum";

		// Token: 0x040000C1 RID: 193
		public static readonly string QS_Select_LookupListEnglishWithFirstBlankItem = "SELECT NULL AS lookuplistid,NULL AS lookupgroupid,NULL AS lookuptext,-999 AS ordernum,'' AS children UNION SELECT lookuplistid,lookupgroupid,lookuptext,ordernum,children FROM lookuplists WHERE lookupgroupid=@lookupgroupid ORDER BY ordernum,lookuptext";

		// Token: 0x040000C2 RID: 194
		public static readonly string QS_Select_LookupListFrenchWithFirstBlankItem = "SELECT NULL AS lookuplistid,NULL AS lookupgroupid,NULL AS lookuptext,-999 AS ordernum,'' AS children UNION SELECT lookuplistid,lookupgroupid,coalesce(nullif(lookupvalue,''),lookuptext) AS lookuptext,ordernum,children FROM lookuplists WHERE lookupgroupid=@lookupgroupid ORDER BY ordernum,lookuptext";

		// Token: 0x040000C3 RID: 195
		public static readonly string QS_Select_LookupListEnglishNoFirstBlankItem = "SELECT lookuplistid,lookupgroupid,lookuptext,ordernum,children FROM lookuplists WHERE lookupgroupid=@lookupgroupid ORDER BY ordernum,lookuptext";

		// Token: 0x040000C4 RID: 196
		public static readonly string QS_Select_LookupListFrenchNoFirstBlankItem = "SELECT lookuplistid,lookupgroupid,coalesce(nullif(lookupvalue,''),lookuptext) AS lookuptext,ordernum,children FROM lookuplists WHERE lookupgroupid=@lookupgroupid ORDER BY ordernum,lookuptext";

		// Token: 0x040000C5 RID: 197
		public static readonly string QS_Select_LookupListChildren = "SELECT childlist FROM lookupgroups WHERE lookupgroupid=@lookupgroupid";

		// Token: 0x040000C6 RID: 198
		public static readonly string QS_Select_PSData_For_LOA = "SELECT    pd.personid,pd.controlid,pd.controlcaption,pd.valtext,pd.valbytesisencrypted\r\n    ,pd.valint\r\n    ,COALESCE(ii.controlvalue,pd.valbytes) AS valbytes\r\n    ,pd.valdate\r\n    ,pd.controlcaption,dc.setting4string\r\n    ,pd.setting1,pd.setting2,pd.setting3,pd.setting4,pd.defaultvalue,pd.controlcode\r\n    ,dc.controlname,pd.valimage\r\nFROM        perstudentdata2 pd LEFT JOIN dynamiccontrols dc ON dc.controlid=pd.controlid \r\n\t\t\tLEFT JOIN ImageInfoPS ii ON ii.PersonID=pd.PersonID AND ii.ControlID=pd.ControlID\r\nWHERE       pd.personid=@pid \r\n            AND pd.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";

		// Token: 0x040000C7 RID: 199
		public static readonly string QS_Select_PSData_For_LOA_With_Signatures = "SELECT    pd.personid,pd.controlid,pd.controlcaption,pd.valtext,pd.valbytesisencrypted\r\n    ,pd.valint\r\n    ,COALESCE(ii.controlvalue,pd.valbytes) AS valbytes\r\n    ,pd.valdate\r\n    ,pd.controlcaption,dc.setting4string\r\n    ,pd.setting1,pd.setting2,pd.setting3,pd.setting4,pd.defaultvalue,pd.controlcode\r\n    ,dc.controlname,pd.valimage\r\nFROM        perstudentdata2 pd LEFT JOIN dynamiccontrols dc ON dc.controlid=pd.controlid \r\n\t\t\tLEFT JOIN ImageInfoPS ii ON ii.PersonID=pd.PersonID AND ii.ControlID=pd.ControlID\r\nWHERE       pd.personid=@pid\r\n            AND \r\n            (pd.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))\r\n            OR\r\n            pd.controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlname='Student Accommodation Signature' OR controlname='Staff Accommodation Signature')\r\n            )";

		// Token: 0x040000C8 RID: 200
		public static readonly string QS_Select_PSData = "SELECT    pd.personid,pd.controlid,pd.controlcaption,pd.valtext,pd.valbytesisencrypted\r\n    ,pd.valint,pd.valbytes,pd.valdate\r\n    ,pd.controlcaption,dc.setting4string\r\n    ,pd.setting1,pd.setting2,pd.setting3,pd.setting4,pd.defaultvalue,pd.controlcode\r\nFROM        perstudentdata2 pd LEFT JOIN dynamiccontrols dc ON dc.controlid=pd.controlid \r\nWHERE       pd.personid=@pid \r\n            AND pd.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))\r\n";

		// Token: 0x040000C9 RID: 201
		public static readonly string QS_Select_StudentNotes2 = "SELECT TOP @numsamplenotes nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated,nd.notes,nd.lecturedate,sp.firstname,sp.lastname,sp.student_no,nd.issamplenotes,nd.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.subjectid,luc.course,luc.timeofday,luc.section,lucd2.altlookupstring AS instructor,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS CourseDescription,nd.docname AS description,nd.issamplenotes \r\nFROM    notetakerdocument nd LEFT JOIN serviceproviders sp ON sp.serviceproviderid=nd.notetakerid \r\n        LEFT JOIN lucourses luc ON luc.lucourseid=nd.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE nd.notetakerid=@spid \r\n        AND nd.lucourseid=@lucidsp \r\n        --AND nd.issamplenotes=1 \r\n        AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) ) \r\nORDER BY nd.lecturedate";

		// Token: 0x040000CA RID: 202
		public static readonly string QS_Select_StudentNotes = "SELECT DISTINCT x.* FROM\r\n(\r\n    SELECT  DISTINCT nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated\r\n                ,nd.notetakerid,nd.lucourseid,nd.notes,nd.lecturedate \r\n    FROM\tNotetakerDocument nd \r\n    WHERE\tnd.NotetakerID=@spid\r\n\t\t    AND nd.LUCourseId=@splucid -- IN (SELECT serviceproviderlucourseid FROM serviceproviderrequests WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid AND serviceprovidertype=128)\r\n\t\t    AND (@includesamplenotes=1 OR nd.issamplenotes=1)\r\n    /*\r\n    UNION\r\n    SELECT  DISTINCT nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated\r\n                ,nd.notetakerid,nd.lucourseid,nd.notes,nd.lecturedate \r\n    FROM        serviceproviderrequestshistory spr LEFT JOIN notetakerdocument nd ON nd.notetakerid=spr.serviceproviderid\r\n                                                    AND nd.lucourseid=spr.serviceproviderlucourseid\r\n    WHERE   spr.personid=@pid \r\n            AND spr.serviceprovidertype=128 \r\n            AND spr.lucourseid=@lucid\r\n            AND spr.stillactive=1\r\n            AND (@includesamplenotes=1 OR nd.issamplenotes=1) AND NOT spr.serviceproviderid IS NULL \r\n            AND NOT nd.notetakerdocumentid IS NULL\r\n    */\r\n) x\r\nORDER BY x.lecturedate DESC";

		// Token: 0x040000CB RID: 203
		public static readonly string QS_Select_NotetakerStudentsCourses = "SELECT c.lucourseid,luc.term,luc.duration,luc.startdate,luc.enddate\r\n        ,luc.subjectid,lucd.altlookupstring AS subject,luc.course,luc.timeofday\r\n        ,luc.section,spr.serviceproviderid,spr.serviceproviderlucourseid\r\nFROM courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\n        LEFT JOIN serviceproviderrequests spr ON spr.personid=@pid AND spr.lucourseid=@lucid\r\nWHERE   c.personid=@pid AND c.lucourseid=@lucid \r\n        AND NOT spr.serviceproviderid IS NULL AND spr.serviceprovidertype=128";

		// Token: 0x040000CC RID: 204
		public static readonly string QS_Select_ServiceProviderApplicationId = "SELECT spac.serviceproviderapplicationid \r\nFROM serviceproviderapplicationcourses spac \r\nWHERE spac.lucourseid IN (SELECT lucourseid FROM equivalentcourses1(@lucid))\r\nAND spac.serviceprovidertype=128";

		// Token: 0x040000CD RID: 205
		public static readonly string QS_Select_StudentsCourses = "SELECT c.lucourseid,luc.term,luc.duration,luc.startdate,luc.enddate\r\n        ,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section\r\n        ,lucd2.altlookupstring AS instructorname,spr.studentrequested AS notetakerrequired\r\n        ,spr.serviceproviderrequestid,spr.serviceproviderid\r\n        ,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS CourseDescription \r\n        ,COUNT(sph.serviceproviderrequesthistoryid) AS NumHistory,\r\n\t\tCAST(NULL AS int) AS selfregstatus\r\nINTO #t1\r\nFROM courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\n        LEFT JOIN serviceproviderrequests spr ON spr.personid=@pid AND spr.lucourseid=c.lucourseid AND spr.serviceprovidertype=128 \r\n        LEFT JOIN serviceproviderrequestshistory sph ON sph.serviceproviderrequestid=spr.serviceproviderrequestid\r\nWHERE c.personid=@pid AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) )\r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\nGROUP BY c.lucourseid,luc.term,luc.duration,luc.startdate,luc.enddate\r\n        ,lucd.altlookupstring,luc.course,luc.timeofday,luc.section\r\n        ,lucd2.altlookupstring,spr.studentrequested \r\n        ,spr.serviceproviderrequestid,spr.serviceproviderid\r\n        ,lucd.altlookupstring,luc.course,luc.timeofday,luc.section\r\n\r\nUPDATE #t1 SET #t1.selfregstatus = RAN.[status]\r\nFROM #t1 SI INNER JOIN StudentCourseAccommodationRequest RAN ON  RAN.personid=@pid AND RAN.lucourseid=SI.luCourseID\r\n\r\nSELECT * FROM #t1\r\n\r\nDROP TABLE #t1";

		// Token: 0x040000CE RID: 206
		public static readonly string QS_Select_PotentialNotetakers = "SELECT    spac.lucourseid,spa.serviceproviderid,sp.firstname,sp.lastname,sp.student_no\r\n                ,spr.serviceproviderrequestid,spr.personid\r\n                ,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS CourseDescription\r\n                ,CAST(0 AS INT) AS activenotetakerothercourse\r\n  FROM      ServiceProviderApplicationCourses spac LEFT JOIN serviceproviderapplications spa ON spa.serviceproviderapplicationid=spac.serviceproviderapplicationid\r\n            LEFT JOIN serviceproviders sp ON sp.serviceproviderid=spa.serviceproviderid\r\n            LEFT JOIN serviceproviderrequests spr ON spr.lucourseid=spac.lucourseid AND spr.serviceproviderid=spa.serviceproviderid AND spr.serviceprovidertype=128\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=spac.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n  WHERE     spac.lucourseid IN (SELECT lucourseid FROM equivalentcourses1(@lucid))\r\n            AND spa.serviceprovidertype=128 \r\n  ORDER BY spa.serviceproviderid,spac.lucourseid";

		// Token: 0x040000CF RID: 207
		public static readonly string QS_Select_PotentialNotetakers_With_Upload_Count = "SELECT    spac.lucourseid,spa.serviceproviderid,sp.firstname,sp.lastname,sp.student_no\r\n                ,spr.serviceproviderrequestid,spr.personid\r\n                ,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS CourseDescription\r\n                ,CAST(0 AS INT) AS activenotetakerothercourse,\r\n\t\t\t\tCOUNT(nd.NotetakerDocumentID) AS NumNotes\r\n  FROM      ServiceProviderApplicationCourses spac LEFT JOIN serviceproviderapplications spa ON spa.serviceproviderapplicationid=spac.serviceproviderapplicationid\r\n            LEFT JOIN serviceproviders sp ON sp.serviceproviderid=spa.serviceproviderid\r\n            LEFT JOIN serviceproviderrequests spr ON spr.lucourseid=spac.lucourseid AND spr.serviceproviderid=spa.serviceproviderid AND spr.serviceprovidertype=128\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=spac.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n\t\t\tLEFT JOIN notetakerdocument nd ON nd.NotetakerID=spa.serviceproviderid AND nd.LUCourseId=spac.lucourseid\r\n  WHERE     spac.lucourseid IN (SELECT lucourseid FROM equivalentcourses1(@lucid))\r\n            AND spa.serviceprovidertype=128 \r\n GROUP BY spac.lucourseid,spa.serviceproviderid,sp.firstname,sp.lastname,sp.student_no,\r\n\t\t\tspr.serviceproviderrequestid,spr.personid\r\n            ,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section \r\n  ORDER BY spa.serviceproviderid,spac.lucourseid";

		// Token: 0x040000D0 RID: 208
		public static readonly string QS_Select_PotentialNotetakers_ServiceProviderId_With_LuCourseId_And_Upload_Count = "SELECT    spac.lucourseid,spa.serviceproviderid,\r\n            \tCOUNT(nd.NotetakerDocumentID) AS NumNotes\r\n  FROM      ServiceProviderApplicationCourses spac LEFT JOIN serviceproviderapplications spa ON spa.serviceproviderapplicationid=spac.serviceproviderapplicationid\r\n            LEFT JOIN notetakerdocument nd ON nd.NotetakerID=spa.serviceproviderid AND nd.LUCourseId=spac.lucourseid\r\n  WHERE     spac.lucourseid IN (SELECT lucourseid FROM equivalentcourses1(@lucid))\r\n            AND spa.serviceprovidertype=128 \r\n GROUP BY spac.lucourseid,spa.serviceproviderid";

		// Token: 0x040000D1 RID: 209
		public static readonly string QS_Select_ServiceProviderById = "SELECT student_no,firstname,lastname,email FROM serviceproviders WHERE serviceproviderid=@id";

		// Token: 0x040000D2 RID: 210
		public static readonly string QS_Select_ServiceProviderByStudent_no2 = "SELECT student_no,firstname,lastname,email,serviceproviderid FROM serviceproviders WHERE student_no=@snume";

		// Token: 0x040000D3 RID: 211
		public static readonly string QS_Select_Notes2 = "SELECT DISTINCT TOP 3 nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated,nd.notes,nd.lecturedate,sp.firstname,sp.lastname,sp.student_no,nd.issamplenotes,nd.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.subjectid,luc.course,luc.timeofday,luc.section,lucd2.altlookupstring AS instructor,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS CourseDescription,nd.docname AS description \r\nFROM    notetakerdocument nd LEFT JOIN serviceproviders sp ON sp.serviceproviderid=nd.notetakerid \r\n        LEFT JOIN lucourses luc ON luc.lucourseid=nd.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE nd.notetakerid=@spid \r\n        AND nd.lucourseid=@lucid2 \r\n        --AND nd.issamplenotes=1 \r\n        AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) ) \r\nORDER BY nd.lecturedate";

		// Token: 0x040000D4 RID: 212
		public static readonly string QS_Select_NotetakerCourses = "SELECT    sp.serviceproviderid,sp.firstname,sp.lastname,sp.student_no,sp.email\r\n,spac.lucourseid,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section\r\n,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS CourseDescription\r\n,sr.serviceproviderrequestid\r\n,luc.term,luc.duration,luc.startdate,luc.enddate\r\n FROM       serviceproviders sp LEFT JOIN serviceproviderapplications spa ON spa.serviceproviderid=sp.serviceproviderid AND spa.serviceprovidertype=128\r\n            LEFT JOIN serviceproviderapplicationcourses spac ON spac.serviceproviderapplicationid=spa.serviceproviderapplicationid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=spac.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN serviceproviderrequests sr ON sr.serviceproviderlucourseid=spac.lucourseid AND sr.serviceproviderid=sp.serviceproviderid\r\n WHERE      sp.serviceproviderid=@id \r\n            AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) )\r\n ORDER BY   spac.lucourseid";

		// Token: 0x040000D5 RID: 213
		public static readonly string QS_Select_ServiceProviderInfo = "SELECT    sp.serviceproviderid,sp.firstname,sp.lastname,sp.middlename,sp.student_no\r\n            ,sp.email,sp.address,sp.phone1,sp.phone2 \r\n            ,sp.address2,sp.addressactive,sp.address2active,sp.email2\r\nFROM serviceproviders sp WHERE sp.serviceproviderid=@nid";

		// Token: 0x040000D6 RID: 214
		public static readonly string QS_Select_Notes = "SELECT nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated,nd.notetakerid,nd.lucourseid,nd.notes,nd.lecturedate FROM notetakerdocument nd WHERE nd.lucourseid=@lucid AND nd.notetakerid=@spid";

		// Token: 0x040000D7 RID: 215
		public static readonly string QS_Select_NotetakerForCourse = "SELECT serviceproviderrequestid FROM serviceproviderrequests WHERE isactive=1 AND serviceproviderid=@spid AND serviceproviderlucourseid=@lucid";

		// Token: 0x040000D8 RID: 216
		[Obsolete]
		public static readonly string QS_Select_ANotetakersStudents = "SELECT sr.serviceproviderrequestid,p.firstname,p.lastname,p.student_no,oi.controlvalue AS email FROM serviceproviderrequests sr LEFT JOIN people p ON p.personid=sr.personid LEFT JOIN otherinfops oi ON oi.personid=sr.personid AND oi.controlid=@emailcid WHERE sr.isactive=1 AND sr.serviceproviderid=@id AND lucourseid IN (SELECT DISTINCT luc2.lucourseid FROM lucourses luc LEFT JOIN lucourses luc2 ON luc2.subjectid=luc.subjectid AND luc2.course=luc.course WHERE luc.lucourseid=@lucid)";

		// Token: 0x040000D9 RID: 217
		public static readonly string QS_Select_NumberProvidingFor = "SELECT COUNT(*) FROM serviceproviderrequests sr WHERE sr.isactive=1 AND sr.serviceproviderid=@id AND lucourseid IN (SELECT DISTINCT luc2.lucourseid FROM lucourses luc LEFT JOIN lucourses luc2 ON luc2.subjectid=luc.subjectid AND luc2.course=luc.course WHERE luc.lucourseid=@lucid)";

		// Token: 0x040000DA RID: 218
		public static readonly string QS_Select_ServiceProviderByEmail = "SELECT sp.serviceproviderid FROM serviceproviders sp WHERE (sp.email=@emailbytes OR sp.email=@emailbytes2) AND isactive=1 ORDER BY serviceproviderid DESC";

		// Token: 0x040000DB RID: 219
		public static readonly string QS_Select_ServiceProviderByStudent_no = "SELECT serviceproviderid FROM serviceproviders WHERE isactive=1 AND student_no=@sne";

		// Token: 0x040000DC RID: 220
		public static readonly string QS_Select_ServiceProviderByUsername = "SELECT serviceproviderid,student_no FROM serviceproviders WHERE isactive=1 AND (altid=@sne OR altid=@sne2)";

		// Token: 0x040000DD RID: 221
		public static readonly string QS_Select_InstructorByEmail = "SELECT lucoursedataid AS instructorid FROM lucoursedata WHERE lookuplisttype=1 AND email=@email";

		// Token: 0x040000DE RID: 222
		public static readonly string QS_Select_InstructorByEmail2 = "SELECT ui.instructoremail,ui.password,a.altlookupstring,a.phone,a.lucoursedataid FROM userinstructor ui LEFT JOIN lucoursedata a ON a.lookuplisttype=1 AND a.email=ui.instructoremail WHERE ui.instructoremail=@email";

		// Token: 0x040000DF RID: 223
		public static readonly string QS_Select_InstructorByUsername = "SELECT lucoursedataid AS instructorid FROM lucoursedata WHERE username=@username";

		// Token: 0x040000E0 RID: 224
		public static readonly string QS_Select_InstructorByEmailOrUsername = "SELECT lucoursedataid AS instructorid FROM lucoursedata WHERE lookuplisttype=1 AND (email=@email OR username=@username)";

		// Token: 0x040000E1 RID: 225
		public static readonly string QS_Select_AltContactByEmailOrUsername = "SELECT alternatecontactid FROM lucoursealternatecontact WHERE isactive=1 AND (altemail=@email OR altusername=@username)";

		// Token: 0x040000E2 RID: 226
		public static readonly string QS_Login_User = "SELECT ui.personid,ui.pass,p.student_no,pg.groupid FROM people p LEFT JOIN userinfo ui ON ui.personid=p.personid LEFT JOIN peoplegroups pg ON pg.personid=p.personid WHERE p.isactive=1 AND ui.username=@uname AND p.personid IN (SELECT personid FROM peoplegroups WHERE groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,','))) ORDER BY p.personid,pg.groupid";

		// Token: 0x040000E3 RID: 227
		public static readonly string QS_Select_UserByStudent_no = "SELECT p.personid,p.firstname,p.lastname FROM people p WHERE p.isactive=1 AND p.student_no=@snume";

		// Token: 0x040000E4 RID: 228
		public static readonly string QS_Select_UserByDynamicDataEncryptedString = "SELECT oi.personid,oi.controlvalue FROM otherinfops oi WHERE oi.controlid=@cid AND (oi.controlvalue=@be OR oi.controlvalue=@b) AND oi.personid IN (SELECT personid FROM people WHERE isactive=1)";

		// Token: 0x040000E5 RID: 229
		public static readonly string QS_Select_UserByDynamicDataEncryptedString2 = "SELECT oi.personid,oi.controlvalue FROM otherinfops oi WHERE oi.controlid=@cid AND (oi.controlvalue=@be OR oi.controlvalue=@b OR oi.controlvalue=@be2 OR oi.controlvalue=@b2) AND oi.personid IN (SELECT personid FROM people WHERE isactive=1)";

		// Token: 0x040000E6 RID: 230
		public static readonly string QS_Select_StudentInfo = "SELECT p.personid,p.firstname,p.lastname,p.middlename,p.student_no,oi.controlvalue AS email FROM people p LEFT JOIN otherinfops oi ON oi.personid=p.personid AND oi.controlid=@cid WHERE p.personid=@pid";

		// Token: 0x040000E7 RID: 231
		public static readonly string QS_Select_StudentInfo2 = "DECLARE @cid int\r\nSET @cid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=260)\r\n\r\nSELECT p.personid,p.firstname,p.lastname,p.middlename,p.student_no,oi.valtext,oi.valbytes,oi.valbytesisencrypted\r\nFROM people p LEFT JOIN perstudentdata2 oi ON oi.personid=p.personid AND oi.controlid=@cid \r\nWHERE p.personid=@pid";

		// Token: 0x040000E8 RID: 232
		public static readonly string QS_Select_GroupMembership = "SELECT personid FROM peoplegroups WHERE personid=@pid AND groupid=@gid";

		// Token: 0x040000E9 RID: 233
		public static readonly string QS_Select_Password = "SELECT pass FROM userinfo WHERE username=@username AND personid IN (SELECT personid FROM people WHERE isactive=1)";

		// Token: 0x040000EA RID: 234
		public static readonly string QS_Select_StaffPassword = "SELECT pass FROM userinfo WHERE username=@username AND personid IN (SELECT personid FROM people WHERE isactive=1) AND personid IN (SELECT personid FROM peoplegroups WHERE groupid=2 OR groupid=10)";

		// Token: 0x040000EB RID: 235
		public static readonly string QS_Select_UserByPersonId = "SELECT p.personid,p.firstname,p.lastname,p.student_no FROM people p WHERE p.personid=@pid";

		// Token: 0x040000EC RID: 236
		public static readonly string QS_InsertUpdate_CourseAlternateContact = "DECLARE @id int\r\nIF EXISTS(SELECT alternatecontactid FROM lucoursealternatecontact WHERE altemail=@email)\r\nBEGIN\r\n    SET @id = (SELECT alternatecontactid FROM lucoursealternatecontact WHERE altemail=@email)\r\n    UPDATE lucoursealternatecontact SET altname=@name WHERE LEN(@name)>0 AND alternatecontactid=@id;\r\n    UPDATE lucoursealternatecontact SET altphone=@phone WHERE LEN(@phone)>0 AND alternatecontactid=@id;\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucoursealternatecontact(altname,altemail,altphone,altusername,altpermissionlevel,whocreated,datecreated,isactive) \r\n        VALUES (@name,@email,@phone,'',0,-555,getdate(),1);\r\n    SET @id=(SELECT MAX(CAST(SCOPE_IDENTITY() AS int)) FROM lucoursealternatecontact)\r\nEND\r\n\r\nUPDATE lucourses SET alternatecontactid=@id WHERE lucourseid=@lucid";

		// Token: 0x040000ED RID: 237
		public static readonly string QS_Select_CourseTimetableByCourse = "SELECT t.*\r\nFROM t.timetable t\r\nWHERE t.timetabletype='C' AND t.lucourseid=@lucid";

		// Token: 0x040000EE RID: 238
		public static readonly string QS_Select_CourseTimetableByStudent = "SELECT t.*\r\nFROM timetable t\r\nWHERE t.timetabletype='C' AND t.lucourseid IN (SELECT lucourseid FROM courses WHERE personid=@pid AND NOT lucourseid=@lucid AND (registrationstatus IS NULL OR NOT registrationstatus=2))";

		// Token: 0x040000EF RID: 239
		public static readonly string QS_Select_CourseTimetableByStudent2 = "SELECT  luc.StartDate,luc.EndDate,t.*\r\nFROM Courses c LEFT JOIN LUCourses luc ON luc.LUCourseID=c.luCourseID \r\n\t\tLEFT JOIN timetable t ON t.lucourseid=c.luCourseID \r\nWHERE\tc.personID=@pid \r\n\t\tAND NOT c.lucourseid=@lucid \r\n\t\tAND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\n\t\tAND NOT ( ( luc.enddate<@startdate ) OR (luc.startdate > @enddate ) )\r\n\t\tAND NOT t.timetableid IS NULL";

		// Token: 0x040000F0 RID: 240
		public static readonly string QS_Select_CourseByInstructor = "DECLARE @enddate2 datetime\r\nSET @enddate2=(SELECT TOP 1 startdate FROM LUCourses WHERE StartDate BETWEEN @enddate AND DATEADD(day,30,@enddate) ORDER BY StartDate)\r\nSET @enddate=coalesce(@enddate2,@enddate)\r\n\r\nSELECT DISTINCT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\n            luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\n            lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\n            lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session, \r\n            lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS coursedescription,\r\n            CASE WHEN EXISTS(SELECT examid FROM exams WHERE lucourseid=luc.lucourseid AND dateoftest>=getdate())\r\n                THEN CAST(1 AS bit) \r\n                ELSE CAST(0 AS bit) \r\n            END AS HasTest\r\nFROM lucourses luc \r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\t(luc.instructorid=@iid OR luc.lucourseid IN (SELECT lucourseid FROM lucourseinstructor WHERE instructorid=@iid) )\r\n        AND NOT ( ( luc.enddate<@startdate) OR (luc.startdate > @enddate ) )\r\n        AND luc.lucourseid IN (SELECT lucourseid FROM courses WHERE registrationstatus IS NULL OR NOT registrationstatus=2)\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040000F1 RID: 241
		public static readonly string QS_Select_CourseByAltContact = "DECLARE @enddate2 datetime\r\nSET @enddate2=(SELECT TOP 1 startdate FROM LUCourses WHERE StartDate BETWEEN @enddate AND DATEADD(day,30,@enddate) ORDER BY StartDate)\r\nSET @enddate=coalesce(@enddate2,@enddate)\r\n\r\nSELECT DISTINCT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\n            luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\n            lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\n            lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session, \r\n            lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS coursedescription \r\n            ,ac.altpermissionlevel,\r\n            CASE WHEN EXISTS(SELECT examid FROM exams WHERE lucourseid=luc.lucourseid AND dateoftest>=getdate())\r\n                THEN CAST(1 AS bit) \r\n                ELSE CAST(0 AS bit) \r\n            END AS HasTest\r\nFROM lucourses luc LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tNOT ( ( luc.enddate<@startdate) OR (luc.startdate > @enddate ) )\r\n        AND (luc.alternatecontactid=@altcontactid OR luc.lucourseid IN (SELECT lucourseid FROM lucoursealtcontact WHERE alternatecontactid=@altcontactid))\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040000F2 RID: 242
		public static readonly string QS_Select_CourseByAltContact_ExcludeCoursesWhereNoClassTestDefinitionExists = "SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\n            luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\n            lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\n            lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session, \r\n            lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS coursedescription \r\n            ,ac.altpermissionlevel\r\nFROM lucourses luc LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tNOT ( ( luc.enddate<@startdate) OR (luc.startdate > @enddate ) )\r\n        AND (luc.alternatecontactid=@altcontactid OR luc.lucourseid IN (SELECT lucourseid FROM lucoursealtcontact WHERE alternatecontactid=@altcontactid))\r\n        AND luc.lucourseid IN (SELECT lucourseid FROM exams)\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040000F3 RID: 243
		public static readonly string QS_Select_CourseByInstructor_ExcludeCoursesWhereNoClassTestDefinitionExists = "SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\n            luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\n            lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\n            lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session, \r\n            lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS coursedescription \r\nFROM lucourses luc \r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\t(luc.instructorid=@iid OR luc.lucourseid IN (SELECT lucourseid FROM lucourseinstructor WHERE instructorid=@idd))\r\n        AND NOT ( ( luc.enddate<@startdate) OR (luc.startdate > @enddate ) )\r\n        AND luc.lucourseid IN (SELECT lucourseid FROM exams)\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040000F4 RID: 244
		public static readonly string QS_Select_FindSubjectId = "SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=0 AND altlookupstring=@subject";

		// Token: 0x040000F5 RID: 245
		public static readonly string QS_Select_FindCourseBySubjectCourseSection = "SELECT lucourseid FROM lucourses WHERE subjectid IN (SELECT lucoursedataid AS subjectid FROM lucoursedata WHERE lookuplisttype=0 AND altlookupstring=@subject) AND course=@course AND section=@section AND NOT ( ( enddate<@sdate ) OR (startdate > @edate ) )";

		// Token: 0x040000F6 RID: 246
		public static readonly string QS_SelectStudentCourse2 = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\n    luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\n    luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\n    lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\n    lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\n    LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n    LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tc.personid=@personid AND c.lucourseid=@lucid AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040000F7 RID: 247
		public static readonly string QS_Select_StudentCourse = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,vpi.instructorname AS instructor,\r\nvpi.instructoremail,vpi.instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN vinstructorprimarylist vpi ON vpi.lucourseid=c.lucourseid\r\nWHERE\tc.personid=@personid AND luc.enddate >= @startdate AND c.lucourseid=@lucid AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040000F8 RID: 248
		public static readonly string QS_Select_StudentCourseMultiple = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,vpi.instructorname AS instructor,\r\nvpi.instructoremail,vpi.instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN vinstructorprimarylist vpi ON vpi.lucourseid=c.lucourseid\r\nWHERE\tc.personid=@personid\r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\n        AND c.lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040000F9 RID: 249
		public static readonly string QS_Select_StudentCourses = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,vpi.instructorname AS instructor,\r\nvpi.instructoremail,vpi.instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN vinstructorprimarylist vpi ON vpi.lucourseid=c.lucourseid\r\nWHERE\tc.personid=@personid AND luc.enddate >= @startdate \r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040000FA RID: 250
		public static readonly string QS_Select_StudentCoursesCurrentTerm = "DECLARE @sdate datetime\r\nDECLARE @edate datetime\r\nDECLARE @now datetime\r\nSET @now = getdate()\r\nDECLARE @m int\r\nSET @m = MONTH(@now)\r\n\r\nif @m>=9 \r\nbegin\r\n   SET @sdate = cast(year(@now) AS char(4)) + '-09-01'\r\n   SET @edate = cast(year(@now) AS char(4)) + '-12-31'\r\nend\r\nelse if @m>=5\r\nbegin\r\n   SET @sdate = cast((year(@now)) AS char(4)) + '-05-01'\r\n   SET @edate = cast((year(@now)) AS char(4)) + '-08-30'\r\nend\r\nelse \r\nbegin\r\n   SET @sdate = cast(year(@now) AS char(4)) + '-01-01'\r\n   SET @edate = cast((year(@now)) AS char(4)) + '-04-30'\r\nend\r\n\r\nSELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\nlucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tc.personid=@pid \r\n\t\tAND (NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) ))\r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\n        AND (@campuses='' OR luc.campus IS NULL OR luc.campus='' OR (NOT luc.campus IS NULL AND luc.campus IN (SELECT orderid AS campus FROM splitstrings(@campuses))))\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040000FB RID: 251
		public static readonly string QS_Select_StudentCoursesRestrictByCampus = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\nlucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tc.personid=@personid AND luc.enddate >= @startdate \r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\n        AND (@campuses='' OR luc.campus IS NULL OR luc.campus='' OR (NOT luc.campus IS NULL AND luc.campus IN (SELECT orderid AS campus FROM splitstrings(@campuses))))\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040000FC RID: 252
		public static readonly string QS_Select_StudentCoursesAndFinalExamDates = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\nlucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\n,e.dateoftest,e.testduration\r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nLEFT JOIN exams e ON e.lucourseid=c.lucourseid AND e.dateoftest>=@startdate AND e.dateoftest<=@enddate AND e.typecode='F'\r\nWHERE\tc.personid=@personid AND luc.enddate >= @startdate AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";

		// Token: 0x040000FD RID: 253
		public static readonly string QS_Select_StudentCourse2 = "SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.course,luc.section,luc.timeofday,lucd2.altlookupstring AS instructorname,lucd2.email AS instructoremail,lucd2.phone AS instructorphone FROM lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid WHERE luc.lucourseid=@lucid";

		// Token: 0x040000FE RID: 254
		public static readonly string QS_Select_CourseInfo = "SELECT    luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term\r\n            ,luc.instructorid\r\n            ,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section\r\n            ,lucd2.altlookupstring AS instructor,lucd2.lookupstring AS instructor2\r\n            ,lucd2.phone AS instructorphone\r\n            ,lucd2.email AS instructoremail,lucd2.username\r\n            ,lucd.email AS subjectemail\r\nFROM        lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\nWHERE       luc.lucourseid=@lucid";

		// Token: 0x040000FF RID: 255
		public static readonly string QS_Select_Template = "SELECT emisc,ebodypdf FROM emailtemplates WHERE templateid=@templateid";

		// Token: 0x04000100 RID: 256
		public static readonly string QS_Select_EveryoneSetting = "SELECT settingstringvalue,settingvalue FROM settingsgroups WHERE settingcode=@settingcode";

		// Token: 0x04000101 RID: 257
		public static readonly string QS_Update_SelectANotetaker = "UPDATE serviceproviderrequests SET serviceproviderid=@spid,dateassigned=getdate(),serviceproviderlucourseid=@splucid WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid AND serviceprovidertype=128";

		// Token: 0x04000102 RID: 258
		public static readonly string QS_Update_ApplicationCourseDateCancelled = "UPDATE serviceproviderapplicationcourses SET datecancelled=null WHERE serviceproviderapplicationcourseid=@id";

		// Token: 0x04000103 RID: 259
		public static readonly string QS_Update_MarkAllBookingsAsNonTentative = "UPDATE appointments SET appcode=0\r\n    WHERE appointmentid IN (SELECT ac.appointmentid FROM exams e LEFT JOIN appointmentcourses ac \r\n                                    ON DATEADD(dd, DATEDIFF(dd,0,e.dateoftest), 0)=DATEADD(dd, DATEDIFF(dd,0,ac.originalstartdatetime), 0)\r\n\t\t\t\t\t\t\t\t\t\tAND ac.LUCourseID=e.lucourseid \r\n                            WHERE e.examid=@examid)";

		// Token: 0x04000104 RID: 260
		public static readonly string QS_Update_ExamInfo = "UPDATE exams SET \r\n    lucourseid=@lucid,lastmodified=getdate(),dateoftest=@dateoftest,wholastmodified=@iid,testduration=@testduration,instructoracknowledged=@instructoracknowledged\r\n    WHERE examid=@examid";

		// Token: 0x04000105 RID: 261
		public static readonly string QS_Update_WaitingListEntry = "UPDATE waitinglist SET skippedreason=@reason WHERE waitinglistid=@id";

		// Token: 0x04000106 RID: 262
		public static readonly string QS_UPDATE_UpdateInstructorPassword = "UPDATE userinstructor SET password=@pwd WHERE instructoremail=@email";

		// Token: 0x04000107 RID: 263
		public static readonly string QS_UPDATE_UpdateStudentNumber = "UPDATE people SET student_no=@sne WHERE personid=@pid";

		// Token: 0x04000108 RID: 264
		public static readonly string QS_UPDATE_UpdateDynamicDataPSDateTime1 = "UPDATE datetimeinfops SET controlvalue=@cv WHERE personid=@pid AND controlid=@pid";

		// Token: 0x04000109 RID: 265
		public static readonly string QS_INSERT_UpdateDynamicDataPSDateTime2 = "INSERT INTO datetimeinfops (screennum,controlid,controlvalue,personid) SELECT 0,@cid,@cv,@pid WHERE NOT EXISTS(SELECT dataid FROM datetimeinfops WHERE personid=@pid AND controlid=@cid)";

		// Token: 0x0400010A RID: 266
		public static readonly string QS_UPDATE_UpdateDynamicDataPSOtherInfo1 = "UPDATE otherinfops SET controlvalue=@cv WHERE personid=@pid AND controlid=@pid";

		// Token: 0x0400010B RID: 267
		public static readonly string QS_INSERT_UpdateDynamicDataPSOtherInfo2 = "INSERT INTO otherinfops (screennum,controlid,controlvalue,personid) SELECT 0,@cid,@cv,@pid WHERE NOT EXISTS(SELECT dataid FROM otherinfops WHERE personid=@pid AND controlid=@cid)";

		// Token: 0x0400010C RID: 268
		public static readonly string QS_INSERTUPDATE_InsertOrUpdateDynamicDataPSOther = "IF EXISTS(SELECT dataid FROM otherinfops WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE otherinfops SET controlvalue=@cv WHERE personid=@pid AND controlid=@cid\r\n    SELECT dataid FROM otherinfops WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfops (screennum,controlid,controlvalue,personid) SELECT 0,@cid,@cv,@pid WHERE NOT EXISTS(SELECT dataid FROM otherinfops WHERE personid=@pid AND controlid=@cid);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int);\r\nEND";

		// Token: 0x0400010D RID: 269
		public static readonly string QS_UPDATE_UnassignNotetaker1 = "INSERT INTO serviceproviderrequestshistory (ServiceProviderRequestID,personid,lucourseid,datetimerequesttitle,startdatetimerequest,enddatetimerequest,serviceprovidertype,dateentered,startdate,enddate,whoentered,ServiceProviderId,ServiceProviderRequestDetailId,notes,studentrequested,studentrequestedcancelnote,DateAssigned,SpecialInstructions,partsgroupid,partsdescription,serviceproviderlucourseid,stillactive) SELECT ServiceProviderRequestID,personid,lucourseid,datetimerequesttitle,startdatetimerequest,enddatetimerequest,serviceprovidertype,dateentered,startdate,enddate,whoentered,ServiceProviderId,ServiceProviderRequestDetailId,notes,studentrequested,studentrequestedcancelnote,DateAssigned,SpecialInstructions,partsgroupid,partsdescription,serviceproviderlucourseid,@stillactive FROM serviceproviderrequests WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid AND serviceprovidertype=128";

		// Token: 0x0400010E RID: 270
		public static readonly string QS_UPDATE_UnassignNotetaker2 = "UPDATE serviceproviderrequests SET dateassigned=NULL,serviceproviderid=NULL,studentrequested=0,studentrequestedcancelnote=@note,serviceproviderlucourseid=NULL WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid AND serviceprovidertype=128";

		// Token: 0x0400010F RID: 271
		public static readonly string QS_UPDATE_MarkStudentAsOfficiallyRequested = "IF NOT EXISTS(SELECT serviceproviderrequestid FROM serviceproviderrequests WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid AND serviceprovidertype=128)\r\nBEGIN\r\n    DECLARE @detailid int\r\n    IF EXISTS(SELECT serviceproviderrequestdetailid FROM serviceproviderrequests WHERE isactive=1 AND personid=@pid AND startdate>=@sdate AND startdate<=@edate AND serviceprovidertype=128)\r\n        SET @detailid = (SELECT TOP 1 serviceproviderrequestdetailid FROM serviceproviderrequests WHERE isactive=1 AND personid=@pid AND startdate>=@sdate AND startdate<=@edate)\r\n    ELSE\r\n    BEGIN\r\n\t\tINSERT INTO serviceproviderrequestdetail (counsellorpid) VALUES (@pid);\r\n\t\tSET @detailid=(SELECT CAST(SCOPE_IDENTITY() AS int))\r\n\tEND\r\n\r\n    INSERT INTO serviceproviderrequests (serviceproviderrequestdetailid,personid,lucourseid,serviceprovidertype,startdate,enddate,whoentered,studentrequested) VALUES (@detailid,@pid,@lucid,128,@sdate,@edate,@pid,1)\r\nEND\r\nELSE\r\nBEGIN \r\n    UPDATE serviceproviderrequests SET studentrequested=1 WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid\r\nEND";

		// Token: 0x04000110 RID: 272
		public static readonly string QS_UPDATE_UpdateServiceProviderCancelDate = "UPDATE serviceproviderapplicationcourses SET datecancelled=getdate(),note=@note WHERE lucourseid=@lucid AND serviceproviderapplicationid IN (SELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@id)";

		// Token: 0x04000111 RID: 273
		public static readonly string QS_UPDATE_RemoveServiceProviderFromAllRequests = "UPDATE serviceproviderrequests SET serviceproviderid=NULL,dateassigned=NULL\r\n            WHERE isactive=1 AND serviceproviderid=@id AND serviceproviderlucourseid=@lucid AND serviceprovidertype=@sptype";

		// Token: 0x04000112 RID: 274
		public static readonly string QS_UPDATE_RemoveServiceProviderAvailability = "DELETE FROM serviceproviderapplicationcourses \r\n            WHERE lucourseid=@lucid AND serviceprovidertype=@sptype AND serviceproviderapplicationid IN (SELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@id)";

		// Token: 0x04000113 RID: 275
		public static readonly string QS_INSERT_ActivateStudent = "INSERT INTO peoplepreviousyears (personid,dateactive) VALUES (@pid,@now)";

		// Token: 0x04000114 RID: 276
		public static readonly string QS_INSERT_PersonRow = "INSERT INTO people (student_no,firstname,middlename,lastname,dateadded,isactive) VALUES (@sne,@fne,@mne,@lne,@now,@true); SELECT CAST(@@IDENTITY AS int);";

		// Token: 0x04000115 RID: 277
		public static readonly string QS_INSERT_AddUserToGroup = "INSERT INTO peoplegroups (personid,groupid,isprimarygroup) VALUES (@pid,@gid,@primarygroup)";

		// Token: 0x04000116 RID: 278
		public static readonly string QS_INSERT_NewServiceProviderApplicationCourse = "IF EXISTS(SELECT serviceproviderapplicationcourseid FROM serviceproviderapplicationcourses WHERE serviceproviderapplicationid=@spa AND serviceprovidertype=@sptype AND lucourseid=@lucid)\r\nBEGIN\r\n    SELECT serviceproviderapplicationcourseid FROM serviceproviderapplicationcourses WHERE serviceproviderapplicationid=@spa AND serviceprovidertype=@sptype AND lucourseid=@lucid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO serviceproviderapplicationcourses \r\n            (serviceproviderapplicationid,serviceprovidertype,lucourseid,datecancelled) \r\n    VALUES  (@spa,@sptype,@lucid,NULL);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS serviceproviderapplicationcourseid\r\nEND";

		// Token: 0x04000117 RID: 279
		public static readonly string QS_UPDATE_ServiceProvider = "UPDATE serviceproviders SET \r\n    firstname=@firstname,middlename=@middlename,lastname=@lastname,student_no=@student_no\r\n    ,email=@email,phone1=@phone1,phone2=@phone2,address=@address,address2=@address2 \r\n    ,addressactive=@addressactive,address2active=@address2active,email2=@email2\r\nWHERE serviceproviderid=@nid";

		// Token: 0x04000118 RID: 280
		public static readonly string QS_INSERT_NewServiceProviderApplication = "IF EXISTS(SELECT serviceproviderid FROM serviceproviderapplications WHERE serviceproviderid=@spid AND serviceprovidertype=@sptype)\r\n\tSELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@spid AND serviceprovidertype=@sptype\r\n\tELSE \r\n\tBEGIN\r\nINSERT INTO serviceproviderapplications (serviceproviderid,serviceprovidertype,note1,note2,dateentered,whoentered,ispermanent,isactive,isactivecomment) \r\nSELECT @spid,@sptype,'','',getdate(),1,NULL,1,NULL  \r\nSELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderapplicationid=@@identity\r\nEND";

		// Token: 0x04000119 RID: 281
		public static readonly string QS_INSERT_NewServiceProviderApplicationInTerm = "IF EXISTS(SELECT serviceproviderid FROM serviceproviderapplications WHERE serviceproviderid=@spid AND serviceprovidertype=@sptype AND (dateentered BETWEEN @termstartdate AND @termenddate) )\r\n\tSELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@spid AND serviceprovidertype=@sptype AND (dateentered BETWEEN @startdate AND @enddate)\r\n\tELSE \r\n\tBEGIN\r\nDECLARE @newdate DATETIME\r\nIF getdate() BETWEEN @termstartdate AND @termenddate\r\n    SET @newdate=getdate()\r\nELSE\r\n    SET @newdate=DATEADD(day,-1,@termenddate)\r\n\r\nINSERT INTO serviceproviderapplications (serviceproviderid,serviceprovidertype,note1,note2,dateentered,whoentered,ispermanent,isactive,isactivecomment) \r\nSELECT @spid,@sptype,'','',@newdate,1,NULL,1,NULL  \r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS serviceproviderapplicationid\r\nEND";

		// Token: 0x0400011A RID: 282
		public static readonly string QS_INSERT_Notes = "INSERT INTO NotetakerDocument (docName, numPages, sizeInBytes, dateCreated, binaryData, NoteTakerId, LUCourseId, notes, lectureDate, issamplenotes) VALUES(@docName, @numPages, @sizeInBytes, @dateCreated, @binaryData, @NoteTakerID, @LUCourseId, @notes, @lectureDate, @issamplenotes)";

		// Token: 0x0400011B RID: 283
		public static readonly string QS_INSERT_AddToWaitingList = "INSERT INTO waitinglist (personid,appointmentid,dateadded,whoadded,apptypeid) VALUES (@pid,@appid,getdate(),@pid,@apptypeid); SELECT waitinglistid FROM waitinglist WHERE waitinglistid=@@identity";

		// Token: 0x0400011C RID: 284
		public static readonly string QS_INSERT_NewAttendee = "INSERT INTO attendees (personid,appointmentid,noshow,misccode) VALUES (@pid,@appid,0,-1)";

		// Token: 0x0400011D RID: 285
		public static readonly string QS_INSERT_NewTestDefinition = "INSERT INTO exams (dateentered,whoentered,lucourseid,description,dateoftest,visible,usercomment,testduration,lastmodified,wholastmodified)\r\nSELECT getdate(),@pid,@lucid,'',@dateoftest,1,'',@testduration,getdate(),@pid\r\nWHERE NOT EXISTS (SELECT examid FROM exams WHERE lucourseid=@lucid AND dateoftest>=@sd AND dateoftest<=@ed)";

		// Token: 0x0400011E RID: 286
		public static readonly string QS_INSERT_CreateTest = "INSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,appcode,groupcode,examid) \r\nSELECT @apptypeid,@startdate,@enddate,@cancelled,@dateadded,@personid,@ishidden,@islocked,@extraattendeescount,@appcode,@groupcode,@examid\r\nWHERE (@dontcareifroombooked=1 OR NOT EXISTS (SELECT att.personid FROM attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid WHERE att.personid=@rid AND app.cancelled=0 AND app.startdate>=@sdmn AND app.startdate<@edmn))\r\nAND NOT EXISTS (SELECT att.personid FROM attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid LEFT JOIN appointmentcourses ac ON ac.appointmentid=att.appointmentid WHERE att.personid=@pid AND app.cancelled=0 AND ac.lucourseid=@lucid AND app.startdate>=@sdmn AND app.startdate<@edmn) \r\nAND NOT EXISTS (SELECT appointmentid FROM apps WHERE PersonID=@pid AND cancelled=0 AND NOT ( ( enddate<@startdate ) OR (startdate > @enddate ) ) )\r\n; SELECT @@IDENTITY;";

		// Token: 0x0400011F RID: 287
		public static readonly string QS_INSERT_CreateTest2 = "DECLARE @sd0 datetime, @sd1 datetime\r\nSET @sd0 = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nSET @sd1 = DATEADD(day,1,@sd0)\r\n\r\nIF EXISTS(SELECT a.appointmentid FROM apps a WHERE a.PersonID=@pid AND a.cancelled=0 AND NOT ( ( a.enddate<=@startdate ) OR (a.startdate >= @enddate ) ))\r\n\tSELECT 0 AS appid,'studentbooked' AS failedreason\r\nELSE IF @dontcareifroombooked=0 AND EXISTS(SELECT a.appointmentid FROM apps a WHERE @dontcareifroombooked=0 AND @rid>0 AND a.PersonID=@rid AND a.cancelled=0 AND NOT ( ( a.enddate<=@startdate ) OR (a.startdate >= @enddate ) ))\r\n\tSELECT 0 AS appid,'roombooked' AS failedreason\r\nELSE IF EXISTS( SELECT ac.appointmentid FROM apps a LEFT JOIN AppointmentCourses ac ON ac.AppointmentID=a.AppointmentID WHERE a.personid=@pid AND a.cancelled=0 AND a.startDate >=@sd0 AND a.startDate<@sd1 AND NOT ac.AppointmentID IS NULL AND ac.LUCourseID=@lucid )\r\n\tSELECT 0 AS appid,'alreadybookedsamecoursesameday' AS failedreason\r\nELSE\r\nBEGIN\r\n\tINSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,appcode,groupcode,examid) \r\n\t\tSELECT @apptypeid,@startdate,@enddate,0,getdate(),@pid,0,0,0,@appcode,-1,@examid\r\n\t\t\r\n\tSELECT CAST(@@IDENTITY AS int) AS appid,'' AS failedreason\r\nEND";

		// Token: 0x04000120 RID: 288
		public static readonly string QS_INSERT_CreateTest3 = "DECLARE @sd0 datetime, @sd1 datetime\r\nSET @sd0 = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nSET @sd1 = DATEADD(day,1,@sd0)\r\n\r\nIF @ignoreapps=0 AND EXISTS(SELECT a.appointmentid FROM apps a WHERE a.PersonID=@pid AND a.cancelled=0 AND NOT ( ( a.enddate<=@startdate ) OR (a.startdate >= @enddate ) ))\r\n\tSELECT 0 AS appid,'studentbooked' AS failedreason\r\nELSE IF @dontcareifroombooked=0 AND EXISTS(SELECT a.appointmentid FROM apps a WHERE @dontcareifroombooked=0 AND @rid>0 AND a.PersonID=@rid AND a.cancelled=0 AND NOT ( ( a.enddate<=@startdate ) OR (a.startdate >= @enddate ) ))\r\n\tSELECT 0 AS appid,'roombooked' AS failedreason\r\nELSE IF @ignoresametestsameday=0 AND EXISTS( SELECT ac.appointmentid FROM apps a LEFT JOIN AppointmentCourses ac ON ac.AppointmentID=a.AppointmentID WHERE a.personid=@pid AND a.cancelled=0 AND a.startDate >=@sd0 AND a.startDate<@sd1 AND NOT ac.AppointmentID IS NULL AND ac.LUCourseID=@lucid )\r\n\tSELECT 0 AS appid,'alreadybookedsamecoursesameday' AS failedreason\r\nELSE\r\nBEGIN\r\n\tINSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,appcode,groupcode,examid,totalbreakminutes) \r\n\t\tSELECT @apptypeid,@startdate,@enddate,0,getdate(),@pid,0,0,0,@appcode,-1,@examid,@totalbreakminutes\r\n\t\t\r\n\tSELECT CAST(@@IDENTITY AS int) AS appid,'' AS failedreason\r\nEND";

		// Token: 0x04000121 RID: 289
		public static readonly string QS_INSERTUPDATE_GetExam = "IF NOT EXISTS(SELECT examid FROM exams WHERE lucourseid=@lucid AND dateoftest>=@sdate AND dateoftest<@edate)\r\nBEGIN\r\n    INSERT INTO exams (dateentered,whoentered,lucourseid,description,dateoftest,testduration,lastmodified,wholastmodified,visible,usercomment,typecode) VALUES (getdate(),-555,@lucid,'',@dateoftest,@testduration,getdate(),NULL,1,NULL,'N')\r\nEND\r\nSELECT examid FROM exams WHERE lucourseid=@lucid AND dateoftest>=@sdate AND dateoftest<@edate";

		// Token: 0x04000122 RID: 290
		public static readonly string QS_INSERTUPDATE_GetExam2 = "IF NOT EXISTS(SELECT examid FROM exams WHERE lucourseid=@lucid AND dateoftest>=@sdate AND dateoftest<@edate)\r\nBEGIN\r\n    INSERT INTO exams (dateentered,whoentered,lucourseid,description,dateoftest,testduration,lastmodified,wholastmodified,visible,usercomment,typecode) VALUES (getdate(),-555,@lucid,'',@dateoftest,@testduration,getdate(),NULL,1,NULL,@testtype)\r\nEND\r\nSELECT examid FROM exams WHERE lucourseid=@lucid AND dateoftest>=@sdate AND dateoftest<@edate";

		// Token: 0x04000123 RID: 291
		public static readonly string QS_Insert_Attendee = "INSERT INTO attendees (appointmentid,personid,noshow,misccode) VALUES (@appid,@pid,@noshow,@misccode)";

		// Token: 0x04000124 RID: 292
		public static readonly string QS_Insert_AppointmentCourses = "INSERT INTO appointmentcourses (appointmentid,lucourseid,originalstartdatetime,originalenddatetime,testnote,studentnote) VALUES (@appointmentid,@lucid,@classsd,@classed,@testnote,@studentnote)";

		// Token: 0x04000125 RID: 293
		public static readonly string QS_INSERT_AddCourse = "INSERT INTO lucourses (startdate,enddate,term,duration,subjectid,course,timeofday,section,instructorid,crosslistcode,equivalentcode,coursenote,whoadded,dateadded,location) VALUES (@startdate,@enddate,@term,@duration,@subjectid,@course,@timeofday,@section,@iid,-1,-1,'',@nid,getdate(),''); SELECT lucourseid FROM lucourses WHERE lucourseid=@@identity";

		// Token: 0x04000126 RID: 294
		public static readonly string QS_INSERT_AddSubject = "INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone) VALUES (0,@subject,@subject,'',''); SELECT lucoursedataid FROM lucoursedata WHERE lucoursedataid=@@identity";

		// Token: 0x04000127 RID: 295
		public static readonly string QS_Delete_WaitingListEntry = "DELETE FROM waitinglist WHERE waitinglistid=@id";

		// Token: 0x04000128 RID: 296
		public static readonly string QS_Delete_DeleteAttendee = "DELETE FROM attendees WHERE appointmentid=@appid AND personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)";

		// Token: 0x04000129 RID: 297
		public static readonly string QS_DELETE_Note = "DELETE FROM notetakerdocument WHERE notetakerdocumentid=@id";
	}
}
