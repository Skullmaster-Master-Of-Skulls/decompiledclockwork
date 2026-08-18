using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsCalendar
{
	// Token: 0x02000163 RID: 355
	public class QueryStorageAppointments
	{
		// Token: 0x04000638 RID: 1592
		internal const string QS_USERS_WITH_TIMES_BOOKED_NON_CANCELLED = "DECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startDate))\r\nDECLARE @ed datetime = DATEADD(D, 1, DATEDIFF(D, 0, @endDate))\r\n\r\nSELECT orderid AS personid INTO #tpids FROM splitorderids(@pids,',')\r\n\r\nSELECT  DISTINCT app.appointmentid,app.startdate,app.enddate,att.PersonID\r\nFROM    appointments app LEFT JOIN attendees att ON att.AppointmentID=app.AppointmentID\r\nWHERE   app.startDate>=@sd AND app.startDate<@ed \r\n        AND app.cancelled=0\r\n        AND att.personid IN (SELECT personid FROM #tpids)\r\nORDER BY att.PersonID,app.appointmentid\r\n\r\nDROP TABLE #tpids";

		// Token: 0x04000639 RID: 1593
		internal const string QS_NUM_APPOINTMENTS_WITH_APP_TYPE = "SELECT COUNT(appointmentid) FROM appointments WHERE apptypeid=@apptypeid";

		// Token: 0x0400063A RID: 1594
		internal const string QS_APPOINTMENTID_WITH_NOSHOW_BEFORE_DATE = "SELECT orderid AS apptypeid INTO #t1 FROM splitorderids(COALESCE(@apptypeids,''),',')\r\n\r\nSELECT\tDISTINCT TOP(@maxnum) app.AppointmentID,att.noShow,app.startDate\r\nFROM\tattendees att LEFT JOIN appointments app ON app.AppointmentID=att.AppointmentID \r\nWHERE\tatt.PersonID=@pid \r\n\t\tAND app.cancelled=0 \r\n\t\tAND app.startDate <= @startdate\r\n\t\tAND (@apptypeids IS NULL OR @apptypeids='' OR app.AppTypeID IN (SELECT apptypeid FROM #t1))\r\nORDER BY app.startDate DESC\r\n\r\nDROP TABLE #t1";

		// Token: 0x0400063B RID: 1595
		internal const string QS_NUM_APPOINTMENTS_NON_CANCELLED_IN_FUTURE = "SELECT orderid AS apptypeid INTO #t1 FROM splitorderids(COALESCE(@apptypeids,''),',')\r\n\r\nIF NOT @enddate IS NULL\r\n\tSET @enddate=DATEADD(day,1,DATEADD(D, 0, DATEDIFF(D, 0, @enddate)))\r\n\r\nSELECT\tCOUNT(DISTINCT att.appointmentid)\r\nFROM\tattendees att LEFT JOIN appointments app ON app.AppointmentID=att.AppointmentID \r\nWHERE\tatt.PersonID=@pid AND app.cancelled=0 \r\n\t\tAND app.startDate >= @startdate\r\n\t\tAND (@enddate IS NULL OR app.endDate<@enddate)\r\n\t\tAND (@apptypeids IS NULL OR @apptypeids='' OR app.AppTypeID IN (SELECT apptypeid FROM #t1))\r\n        AND NOT (DATEPART(hh,app.startdate)=0 AND DATEPART(hh,app.enddate)=1 AND DATEPART(n,app.startdate)=0 AND DATEPART(n,app.enddate)=0) --not a poc\r\n        AND (@excludeTestsExams=0 OR app.examid IS NULL OR app.examid<1 )\r\n\r\nDROP TABLE #t1";

		// Token: 0x0400063C RID: 1596
		internal const string QS_APPOINTMENT_ICON_INFO = "SELECT appointmenticoninfoid,iconindex,icontext,iconletteridentifier FROM appointmenticoninfo";

		// Token: 0x0400063D RID: 1597
		internal const string QS_APP_CANCEL_REASONS = "SELECT cr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n         cr.cancelreasondescription,cr.colour AS appcancelreasoncolour,\r\n         cr.ordernum AS appcancelreasonordernum,cr.isactive AS appcancelreasonisactive\r\nFROM     cancelreason cr \r\nWHERE    cr.isactive=1\r\nORDER BY cr.cancelreasongroupname,cr.ordernum,cr.cancelreasontitle";

		// Token: 0x0400063E RID: 1598
		internal const string QS_DELETED_APPOINTMENT_BY_ID = "SELECT    @appid AS appointmentid,aa.startdate,aa.enddate,aa.apptypeid,at.[description],\r\n            aa.subject AS subtitle,aatt.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM    archive_appointments aa LEFT JOIN archive_attendees aatt ON aatt.appointmentid=aa.appointmentid\r\n        LEFT JOIN people p ON p.personid=aatt.personid\r\n        LEFT JOIN AppointmentTypes at ON aa.apptypeid=at.apptypeid\r\nWHERE   aa.appointmentid=@appid AND aa.auditaction='DEL'\r\nORDER BY aa.auditdatetime DESC";

		// Token: 0x0400063F RID: 1599
		internal const string QS_SCREENNUM_BY_APPTYPEID = "SELECT perappscreennumsfortabs FROM appointmenttypes WHERE apptypeid=@apptypeid";

		// Token: 0x04000640 RID: 1600
		internal const string QS_ALL_APP_TYPES = "SELECT at.apptypeid,at.description AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,at.appointmenttypegroupid,atg.description AS apptypegrouptitle\r\nFROM    appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\nWHERE at.isactive=1\r\nORDER BY atg.description,at.description";

		// Token: 0x04000641 RID: 1601
		internal const string QS_CLASSTESTDEFINITIONS_BY_COURSE = "SELECT    e.examid,e.dateoftest AS examstartdatetime,e.testduration\r\n            ,e.instructorcontacteddate,e.instructorcontactednote,e.instructoracknowledged\r\n            ,e.usercomment,e.typecode\r\n            ,e.lucourseid AS examlucourseid,e.lucourseid AS examcourselucourseid\r\n            ,luce.startdate AS examcoursestartdate,luce.enddate AS examcourseenddate\r\n            ,luce.duration AS examcourseduration,luce.term AS examcourseterm\r\n            ,luce.subjectid AS examcoursesubjectid,lucde.lookupstring AS examcoursesubjectcode\r\n            ,lucde.email AS examcoursesubjectemail\r\n            ,lucde.altlookupstring AS examcoursesubjectdescription,luce.course AS examcoursecourse\r\n            ,luce.section AS examcoursesection,luce.timeofday AS examcoursetimeofday\r\n            ,luce.campus AS examcoursecampus,luce.location AS examcourselocation\r\n            ,luce.department AS examcoursedepartment\r\n            ,alt.alternatecontactid,alt.altname,alt.altemail,alt.altphone\r\n            ,alt.altusername,alt.altpermissionlevel,\r\n            lucd2.lucoursedataid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,\r\n            lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,\r\n            lucd2.externalid AS pinstructorexternalid,lucd2.id AS pinstructoremployeeid,\r\n            e.filename AS classlocation\r\nFROM        exams e LEFT JOIN lucourses luce ON luce.lucourseid=e.lucourseid\r\n            LEFT JOIN lucoursedata lucde ON lucde.lucoursedataid=luce.subjectid\r\n            LEFT JOIN lucoursealternatecontact alt ON alt.alternatecontactid=luce.alternatecontactid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luce.instructorid\r\nWHERE       e.lucourseid=@lucid AND e.dateoftest>=getdate()\r\nORDER BY    e.dateoftest";

		// Token: 0x04000642 RID: 1602
		internal const string QS_CLASSTESTDEFINITION_BY_EXAMID = "SELECT    e.examid,e.dateoftest AS examstartdatetime,e.testduration\r\n            ,e.instructorcontacteddate,e.instructorcontactednote,e.instructoracknowledged\r\n            ,e.usercomment,e.typecode\r\n            ,e.lucourseid AS examlucourseid,e.lucourseid AS examcourselucourseid\r\n            ,luce.startdate AS examcoursestartdate,luce.enddate AS examcourseenddate\r\n            ,luce.duration AS examcourseduration,luce.term AS examcourseterm\r\n            ,luce.subjectid AS examcoursesubjectid,lucde.lookupstring AS examcoursesubjectcode\r\n            ,lucde.email AS examcoursesubjectemail\r\n            ,lucde.altlookupstring AS examcoursesubjectdescription,luce.course AS examcoursecourse\r\n            ,luce.section AS examcoursesection,luce.timeofday AS examcoursetimeofday\r\n            ,luce.campus AS examcoursecampus,luce.location AS examcourselocation\r\n            ,luce.department AS examcoursedepartment\r\n            ,alt.alternatecontactid,alt.altname,alt.altemail,alt.altphone\r\n            ,alt.altusername,alt.altpermissionlevel,\r\n            lucd2.lucoursedataid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,\r\n            lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,\r\n            lucd2.externalid AS pinstructorexternalid,lucd2.id AS pinstructoremployeeid,\r\n            e.filename AS classlocation\r\nFROM        exams e LEFT JOIN lucourses luce ON luce.lucourseid=e.lucourseid\r\n            LEFT JOIN lucoursedata lucde ON lucde.lucoursedataid=luce.subjectid\r\n            LEFT JOIN lucoursealternatecontact alt ON alt.alternatecontactid=luce.alternatecontactid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luce.instructorid\r\nWHERE       e.examid=@examid\r\nORDER BY    e.dateoftest";

		// Token: 0x04000643 RID: 1603
		internal const string QS_INVIGILATORS = "IF EXISTS(SELECT groupid FROM Groups WHERE description='invigilators')\r\nBEGIN\r\nSELECT    pg.personid,p.firstname,p.lastname,p.student_no,p.middlename,NULL AS groupid,NULL AS description\r\nFROM        groups g LEFT JOIN peoplegroups pg ON pg.groupid=g.groupid\r\n            LEFT JOIN people p ON p.personid=pg.personid\r\nWHERE       g.description='invigilators'\r\nEND\r\nELSE\r\nBEGIN\r\nSELECT pg.personid,p.firstname,p.lastname,p.student_no,p.middlename,NULL AS groupid,NULL AS description\r\nFROM        groups g LEFT JOIN peoplegroups pg ON pg.groupid=g.groupid\r\n            LEFT JOIN people p ON p.personid=pg.personid\r\nWHERE       pg.GroupID IN (SELECT groupid FROM PeopleGroups WHERE NOT PersonID IN (SELECT PersonID FROM PeopleGroups WHERE GroupID<=10))\r\nEND";

		// Token: 0x04000644 RID: 1604
		internal const string QS_TEST_ACCOMMODATIONS_BY_APPOINTMENTID_AND_PERSONID_AND_LUCOURSEID = "DECLARE @courseortemplate int = COALESCE((SELECT dbo.AccommodationsCourseOrTemplate(@pid,@lucid)),0)\r\n\r\nSELECT @pid AS personid,@lucid AS lucourseid,at.AppointmentId\r\n\t\t,0 AS DataID,at.controlcaption,at.controlcode,at.controlid\r\n\t\t,at.setting1,at.setting2,at.setting3,at.setting4,at.defaultvalue\r\n\t\t,COALESCE(at.valtext,ad2.valtext) AS valtext,\r\n\t\tCOALESCE(at.valbytes,ad2.valbytes) AS valbytes,\r\n\t\tCOALESCE(at.valdate,ad2.valdate) AS valdate,\r\n\t\tCOALESCE(at.valint,ad2.valint) AS valint,\r\n\t\tat.valimage,\r\n\t\tCOALESCE(at.valbytesisencrypted,ad2.valbytesisencrypted),\r\n\t\tCAST(1 AS bit) AS UseForTest,\r\n        dc.setting4string,dc.defaultvaluestring,dc.mask,dc.controlgroup\r\nFROM\taccommodationstestdata at LEFT JOIN dynamiccontrols dc ON dc.controlid=at.controlid\r\n\t\tLEFT JOIN accommodationdata ad2 ON ad2.PersonID=at.personid AND ad2.courseid=at.lucourseid AND ad2.ControlID=at.controlid\r\nWHERE\tat.AppointmentId=@appid \r\nUNION\r\nSELECT\tad.PersonID,ad.courseid,@appid AS appointmentid\r\n\t\t,ad.DataID,ad.controlcaption,ad.controlcode,ad.controlid\r\n\t\t,ad.setting1,ad.setting2,ad.setting3,ad.setting4,ad.defaultvalue\r\n\t\t,ad.valtext,ad.valbytes,ad.valdate,ad.valint,NULL AS valimage,ad.valbytesisencrypted \r\n\t\t,CAST(0 AS bit) AS UseForTest\r\n\t\t,dc.setting4string,dc.defaultvaluestring,dc.mask,dc.controlgroup\r\nFROM\taccommodationdata ad LEFT JOIN dynamiccontrols dc ON dc.controlid=ad.controlid\r\nWHERE\tad.PersonID=@pid AND ad.courseid=@courseortemplate\r\n\t\tAND ad.[offline]=0 \r\n\t\tAND (ad.expirydate IS NULL OR ad.expirydate > GETDATE() )\r\n\t\tAND (ad.showonletter & 2 = 2)";

		// Token: 0x04000645 RID: 1605
		internal const string QS_Appointments = "SET ARITHABORT ON \r\nEXECUTE LoadAppointments @pids,@apptypeids,@sd,@ed,@checkpsicons,@checkanicons,@hidecancelled";

		// Token: 0x04000646 RID: 1606
		internal const string QS_APPOINTMENT_BY_ID = "SELECT a.appointmentid,a.apptypeid,a.startdate,a.enddate,a.cancelled\r\n ,att.personid,att.noshow,att.misccode,am.memotext,ai.screennum,ai.iconnum,aif.icontext,aif.iconletteridentifier\r\n ,a.dateadded,a.whoadded,am.isencrypted,a.ishidden,a.islocked,a.overridecolour\r\n ,p.firstname,p.lastname,p.student_no,pg.groupid,aw.workshopid,ac.lucourseid,w.workshoptitle\r\n ,w.maxattendees,lucd.altlookupstring AS subject,lc.course,a.extraattendeescount\r\n ,a.appcode,a.groupcode,ac.originalstartdatetime,ac.originalenddatetime\r\n ,ac.appointmentcourseid,ac.testnote,lucd2.altlookupstring,lucd2.email,lucd2.phone,lc.section\r\n ,ac.studentnote,acr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle\r\n ,acr.cancelreasontext,acr.cancelledbypersonid,acr.cancelleddate \r\n ,a.actualstarttime,a.actualendtime,a.subject AS subtitle,a.location,aw.maxattendees AS appmaxattendees ,a.caseid \r\n ,at.description AS apptypedescription,atg.title AS apptypegrouptitle,at.appointmenttypegroupid\r\n ,at.defaultcolour,att.attendeeid,a.examid\r\n FROM apps a LEFT JOIN appointmentmemos am ON am.appointmentid=a.appointmentid \r\n LEFT JOIN appointmenticons ai ON ai.appointmentid=a.appointmentid \r\n LEFT JOIN AppointmentIconInfo aif ON aif.iconindex=ai.iconnum\r\n LEFT JOIN attendees att ON att.appointmentid=a.appointmentid \r\n LEFT JOIN people p ON p.personid=att.personid \r\n LEFT JOIN peoplegroups pg ON pg.personid=att.personid AND pg.groupid<10 --pg.isprimarygroup=1 \r\n LEFT JOIN appointmentworkshops aw ON aw.appointmentid=a.appointmentid \r\n LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid \r\n LEFT JOIN workshops w ON w.workshopid=aw.workshopid \r\n LEFT JOIN lucourses lc ON lc.lucourseid=ac.lucourseid \r\n LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=lc.subjectid \r\n LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=lc.instructorid \r\n LEFT JOIN appointmentcancelledreason acr ON acr.appointmentid=a.appointmentid LEFT JOIN cancelreason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n LEFT JOIN appointmenttypes at ON at.apptypeid=a.apptypeid\r\n LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\n WHERE a.appointmentid=@appid\r\n ORDER BY a.startdate,a.appointmentid,pg.groupid,a.personid,ai.screennum,ai.iconnum";

		// Token: 0x04000647 RID: 1607
		internal const string QI_RECOVER_DELETED_APPOINTMENT = "INSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,\r\nappcode,groupcode,caseid,examid,totalbreakminutes,sittingid)\r\nSELECT \r\napptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,\r\nappcode,groupcode,caseid,examid,totalbreakminutes,sittingid\r\nFROM archive_appointments \r\nWHERE appointmentid=@appid;\r\n\r\nDECLARE @newappid int\r\nSET @newappid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() As int))\r\n\r\nINSERT INTO attendees(PersonID,AppointmentID,noShow,miscCode) \r\nSELECT personid,@newappid,noshow,misccode FROM archive_attendees \r\nWHERE AppointmentID=@appid;\r\n\r\nINSERT INTO AppointmentCourses(AppointmentID,LUCourseID,originalStartDateTime,originalEndDateTime,testNote,studentNote) \r\nSELECT @newappid,lucourseid,originalStartDateTime,originalEndDateTime,testNote,studentNote\r\nFROM archive_appointmentCourses WHERE AppointmentID=@appid;\r\n\r\nINSERT INTO AppointmentWorkshops(AppointmentID,WorkshopID,PublishOnline,location,maxattendees) \r\nSELECT @newappid,WorkshopID,PublishOnline,location,maxattendees\r\nFROM archive_appointmentWorkshops WHERE AppointmentID=@appid;\r\n\r\nINSERT INTO AppointmentMemos(AppointmentID,memotext,isencrypted)\r\nSELECT @newappid,memotext,isencrypted\r\nFROM archive_appointmentMemos WHERE AppointmentID=@appid;\r\n\r\nSELECT @newappid AS appointmentid";

		// Token: 0x04000648 RID: 1608
		internal const string QI_ACCOMMODATION_FOR_TEST = "IF EXISTS(SELECT examid FROM accommodationstest WHERE examid=@examid AND appointmentid=@appointmentid AND personid=@personid AND controlid=@controlid)\r\nBEGIN\r\n    UPDATE accommodationstest SET valbytes=@valbytes,valint=@valint,valdate=@valdate,@whoselected=@whoselected,datemodified=getdate() WHERE examid=@examid AND appointmentid=@appointmentid AND personid=@personid AND controlid=@controlid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO accommodationstest (examid,personid,controlid,whoselected,datemodified,appointmentid,valbytes,valint,valdate)\r\nVALUES (@examid,@personid,@controlid,@whoselected,getdate(),@appointmentid,@valbytes,@valint,@valdate)\r\nEND";

		// Token: 0x04000649 RID: 1609
		internal const string QI_APPOINTMENT_MEMO = "INSERT INTO appointmentmemos (appointmentid,memotext,isencrypted) VALUES (@appid,@memotext,1)";

		// Token: 0x0400064A RID: 1610
		internal const string QI_APPOINTMENT = "INSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked\r\n                            ,overridecolour,extraattendeescount,appcode,groupcode,actualstarttime,actualendtime\r\n                            ,location,examid,caseid,totalbreakminutes,sittingid,subject)\r\nVALUES (@apptypeid,@startdate,@enddate,@iscancelled,getdate(),@whobooked,@isprivate,@islocked\r\n        ,@overridecolour,@extraattendeescount,@appcode,@groupcode,@actualstarttime,@actualendtime\r\n        ,@location,@examid,@caseid,@breakminutes,@sittingid,@subtitle);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS appointmentid;";

		// Token: 0x0400064B RID: 1611
		internal const string QI_POINT_OF_CONTACT = "INSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked\r\n                            ,overridecolour,extraattendeescount,appcode,groupcode,actualstarttime,actualendtime\r\n                            ,location,examid,caseid,totalbreakminutes,sittingid,subject)\r\nVALUES (@apptypeid,@sdt,@edt,0,getdate(),@staffpid,0,0\r\n        ,NULL,0,0,-1,NULL,NULL\r\n        ,NULL,NULL,NULL,0,NULL,NULL);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS appointmentid;";

		// Token: 0x0400064C RID: 1612
		internal const string QI_ATTENDEE = "DECLARE @rm bit\r\nIF EXISTS(SELECT personid FROM peoplegroups WHERE personid=@pid AND groupid=3)\r\n\tSET @rm = 1\r\nELSE\r\n\tSET @rm = 0\r\nIF NOT EXISTS(SELECT attendeeid FROM attendees WHERE appointmentid=@appid AND personid=@pid)\r\nBEGIN\r\n    INSERT INTO attendees (personid,appointmentid,noshow,misccode) VALUES (@pid,@appid,@noshow,@misccode);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS attendeeid\r\nEND\r\nELSE\r\nBEGIN\r\n    UPDATE attendees SET noshow=@noshow,misccode=@misccode WHERE appointmentid=@appid AND personid=@pid;\r\n    SELECT attendeeid FROM attendees WHERE appointmentid=@appid AND personid=@pid;\r\nEND";

		// Token: 0x0400064D RID: 1613
		internal const string QI_APPOINTMENT_ICON = "INSERT INTO appointmenticons (appointmentid,screennum,iconnum) \r\nVALUES (@appid,@screennum,@iconnum);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS appiconid";

		// Token: 0x0400064E RID: 1614
		internal const string QI_TEST_CLASS_DEFINITION = "INSERT INTO exams (dateentered,whoentered,lucourseid,description,dateoftest,testduration,lastmodified\r\n                    ,wholastmodified,visible,typecode,extendedproperties\r\n                    ,privatenote,instructorcontacteddate\r\n                    ,instructorcontactednote,instructoracknowledged,usercomment,filename) \r\nVALUES (getdate(),@whobooked,@lucid,@description,@dateoftest,@testduration,getdate()\r\n                    ,@whobooked,1,@typecode,''\r\n                    ,@privatenote,@instructorcontacteddate\r\n                    ,@instructorcontactednote,@instructoracknowledged,@usercomment,@location);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS examid";

		// Token: 0x0400064F RID: 1615
		internal const string QI_APPOINTMENT_WORKSHOP = "INSERT INTO appointmentworkshops(appointmentid,workshopid,publishonline,maxattendees)\r\nVALUES (@appid,@workshopid,@publishonline,@maxattendees)";

		// Token: 0x04000650 RID: 1616
		internal const string QU_APPOINTMENT_CASE = "UPDATE appointments SET caseid=@caseid WHERE appointmentid=@appid";

		// Token: 0x04000651 RID: 1617
		internal const string QU_SWAP_ATTENDEE = "UPDATE attendees SET personid=@pidnew WHERE appointmentid=@appid AND personid=@pidold";

		// Token: 0x04000652 RID: 1618
		internal const string QS_MERGE_APPOINTMENTS = "SELECT DISTINCT appointmentid FROM (SELECT appointmentid FROM attendees WHERE personid=@oldpid UNION SELECT appointmentid FROM appointments WHERE personid=@oldpid) x";

		// Token: 0x04000653 RID: 1619
		internal const string QU_MERGE_APPOINTMENTS = "UPDATE attendees SET personid=@newpid WHERE personid=@oldpid;\r\nUPDATE appointments SET personid=@newpid WHERE personid=@oldpid";

		// Token: 0x04000654 RID: 1620
		internal const string QU_APPOINTMENT_CANCEL_INFO = "IF EXISTS(SELECT appointmentid FROM appointmentcancelledreason WHERE appointmentid=@appid)\r\n    UPDATE appointmentcancelledreason SET cancelreasontext=@text,cancelreasonid=@cancelreasonid WHERE appointmentid=@appid\r\nELSE\r\n    INSERT INTO appointmentcancelledreason (appointmentid,cancelreasonid,cancelreasontext,cancelledbypersonid,cancelleddate) VALUES (@appid,@cancelreasonid,@text,@whoami,getdate())";

		// Token: 0x04000655 RID: 1621
		internal const string QU_ATTENDEE_NOSHOW_STATUS = "UPDATE attendees SET noshow=@noshow WHERE appointmentid=@appid AND personid=@pid";

		// Token: 0x04000656 RID: 1622
		internal const string QU_APPOINTMENT_CANCEL_OR_UNCANCEL = "UPDATE appointments SET cancelled=@iscancelled WHERE appointmentid=@appid";

		// Token: 0x04000657 RID: 1623
		internal const string QU_APPOINTMENT_TENTATIVE_OR_UNTENTATIVE = "IF @istentative=1 \r\n    UPDATE appointments SET appcode=-1 WHERE appointmentid=@appid\r\nELSE IF EXISTS(SELECT appointmentid FROM appointments WHERE appcode=-1 AND appointmentid=@appid)\r\n    UPDATE appointments SET appcode=0 WHERE appointmentid=@appid";

		// Token: 0x04000658 RID: 1624
		internal const string QU_CLASS_TEST_DEFINITION = "UPDATE exams SET lucourseid=@lucid,description=@description,dateoftest=@dateoftest,testduration=@testduration\r\n                ,lastmodified=getdate(),usercomment=@usercomment,typecode=@typecode\r\n                ,testpickedupdate=@testpickedupdate,testpickedupnote=@testpickedupnote\r\n                ,privatenote=@privatenote,instructorcontacteddate=@instructorcontacteddate\r\n                ,instructorcontactednote=@instructorcontactednote,instructoracknowledged=@instructoracknowledged,\r\n                filename=@location\r\nWHERE examid=@examid";

		// Token: 0x04000659 RID: 1625
		internal const string QU_EXAM_TESTDELIVERED = "UPDATE exams SET usercomment=@testdeliverednote WHERE examid=@examid";

		// Token: 0x0400065A RID: 1626
		internal const string QU_APPOINTMENT = "UPDATE appointments SET startdate=@startdate,enddate=@enddate,apptypeid=@apptypeid,cancelled=@iscancelled\r\n    ,ishidden=@isprivate,islocked=@islocked,overridecolour=@overridecolour,extraattendeescount=@extraattendeescount\r\n    ,appcode=@appcode,groupcode=@groupcode,actualstarttime=@actualstarttime,actualendtime=@actualendtime\r\n    ,location=@location,examid=@examid,totalbreakminutes=@breakminutes,sittingid=@sittingid\r\n    ,caseid=@caseid,subject=@subtitle\r\nWHERE appointmentid=@appid";

		// Token: 0x0400065B RID: 1627
		internal const string QU_APPOINTMENT_GROUPCODE = "UPDATE appointments SET groupcode=@groupcode WHERE appointmentid=@appid";

		// Token: 0x0400065C RID: 1628
		internal const string QU_APPOINTMENT_WORKSHOP = "UPDATE appointmentworkshops SET workshopid=@workshopid,publishonline=@publishonline,maxattendees=@maxattendees WHERE appointmentid=@appid";

		// Token: 0x0400065D RID: 1629
		internal const string QU_APPOINTMENT_MEMO = "IF EXISTS(SELECT appointmentid FROM appointmentmemos WHERE appointmentid=@appid)\r\n    UPDATE appointmentmemos SET memotext=@memo,isencrypted=1 WHERE appointmentid=@appid\r\nELSE \r\n    INSERT INTO appointmentmemos (appointmentid,memotext,isencrypted) VALUES (@appid,@memo,1)";

		// Token: 0x0400065E RID: 1630
		internal const string QU_ATTENDEE = "IF EXISTS(SELECT personid FROM attendees WHERE appointmentid=@appid AND personid=@pid)\r\n    UPDATE attendees SET noshow=@noshow,misccode=@misccode WHERE appointmentid=@appid AND personid=@pid\r\nELSE\r\n    INSERT INTO attendees (noshow,misccode,appointmentid,personid) VALUES (@noshow,@misccode,@appid,@pid)\r\n\r\nSELECT attendeeid FROM attendees WHERE appointmentid=@appid AND personid=@pid";

		// Token: 0x0400065F RID: 1631
		internal const string QU_SWAP_APPTYPEIDS = "UPDATE appointments SET apptypeid=@apptypeidtokeep WHERE apptypeid=@apptypeidtoreplace";
	}
}
