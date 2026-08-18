using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeCodes
{
	// Token: 0x020002DC RID: 732
	[Serializable]
	public enum eMailMergeCode
	{
		// Token: 0x04001215 RID: 4629
		[MailMergeCode(eMailMergeCodeGroup.Courses, "coursedescription", "Course subject, course code, section and time of day in one line.", typeof(string), "BIOL 101 2 LEC")]
		COURSE_CourseDescription,
		// Token: 0x04001216 RID: 4630
		[MailMergeCode(eMailMergeCodeGroup.Courses, "term", "The course term.", typeof(string), "Fall")]
		COURSE_Term,
		// Token: 0x04001217 RID: 4631
		[MailMergeCode(eMailMergeCodeGroup.Courses, "duration", "The course duration.", typeof(string), "D1")]
		COURSE_Duration,
		// Token: 0x04001218 RID: 4632
		[MailMergeCode(eMailMergeCodeGroup.Courses, "subject", "The subject portion of the course.", typeof(string), "BIOL")]
		COURSE_Subject,
		// Token: 0x04001219 RID: 4633
		[MailMergeCode(eMailMergeCodeGroup.Courses, "subjectemail", "The course subject email.", typeof(string), "department@tpro.ca")]
		COURSE_SubjectEmail,
		// Token: 0x0400121A RID: 4634
		[MailMergeCode(eMailMergeCodeGroup.Courses, "alternatecontactname", "The primary alternate contact name.", typeof(string), "John Smith")]
		COURSE_AlternateContactName,
		// Token: 0x0400121B RID: 4635
		[MailMergeCode(eMailMergeCodeGroup.Courses, "alternatecontactemail", "The primary alternate contact email address.", typeof(string), "john@tpro.ca")]
		COURSE_AlternateContactEmail,
		// Token: 0x0400121C RID: 4636
		[MailMergeCode(eMailMergeCodeGroup.Courses, "alternatecontactphone", "The primary alternate contact phone number.", typeof(string), "123-555-1234")]
		COURSE_AlternateContactPhone,
		// Token: 0x0400121D RID: 4637
		[MailMergeCode(eMailMergeCodeGroup.Courses, "alternatecontactnames", "A comma separated list of all alternate contact names.", typeof(string), "John Smith, Mary Jones, Ralph Richards")]
		COURSE_AlternateContactNames,
		// Token: 0x0400121E RID: 4638
		[MailMergeCode(eMailMergeCodeGroup.Courses, "alternatecontacts", "A comma separated list of all alternate contact names with email and phone", typeof(string), "John Smith; email: john@tpro.ca; phone: 123-555-1234, Mary Jones; email: mary@tpro.ca; phone: 123-555-3321")]
		COURSE_AlternateContacts,
		// Token: 0x0400121F RID: 4639
		[MailMergeCode(eMailMergeCodeGroup.Courses, "alternatecontactemails", "A comma separated list of all alternate contact emails.", typeof(string), "john@tpro.ca, mary@tpro.ca")]
		COURSE_AlternateContactEmails,
		// Token: 0x04001220 RID: 4640
		[MailMergeCode(eMailMergeCodeGroup.Courses, "instructor", "The primary instructor name.", typeof(string), "Smith, J.", new string[]
		{
			"instructorname"
		})]
		COURSE_Instructor,
		// Token: 0x04001221 RID: 4641
		[MailMergeCode(eMailMergeCodeGroup.Courses, "instructorfirstname", "The primary instructor first name.  The instructor name is a single value, so this code will attempt to parse the first name out looking for space or commas.", typeof(string), "J.")]
		COURSE_InstructorFirstName,
		// Token: 0x04001222 RID: 4642
		[MailMergeCode(eMailMergeCodeGroup.Courses, "instructorlastname", "The primary instructor last name. The instructor name is a single value, so this code will attempt to parse the first name out looking for space or commas.", typeof(string), "Smith")]
		COURSE_InstructorLastName,
		// Token: 0x04001223 RID: 4643
		[MailMergeCode(eMailMergeCodeGroup.Courses, "instructoremail", "The primary instructor email.", typeof(string), "smith@tpro.ca")]
		COURSE_InstructorEmail,
		// Token: 0x04001224 RID: 4644
		[MailMergeCode(eMailMergeCodeGroup.Courses, "instructorphone", "The primary instructor phone.", typeof(string), "123-555-3321")]
		COURSE_InstructorPhone,
		// Token: 0x04001225 RID: 4645
		[MailMergeCode(eMailMergeCodeGroup.Courses, "primaryinstructoremails", "A comma separated list of emails for all primary instructors from all courses in scope.", typeof(string), "smith@tpro.ca, jones@tpro.ca")]
		COURSE_PrimaryInstructorEmails,
		// Token: 0x04001226 RID: 4646
		[MailMergeCode(eMailMergeCodeGroup.Courses, "allinstructoremails", "A comma separated list of emails for all instructors from all courses in scope.", typeof(string), "smith@tpro.ca,jones@tpro.ca")]
		COURSE_PrimaryAndSecondaryInstructorEmails,
		// Token: 0x04001227 RID: 4647
		[MailMergeCode(eMailMergeCodeGroup.Courses, "primaryinstructornames", "A comma separated list of names for all primary instructors from all courses in scope.", typeof(string), "John Smith, Mary Roberts")]
		COURSE_PrimaryInstructorNames,
		// Token: 0x04001228 RID: 4648
		[MailMergeCode(eMailMergeCodeGroup.Courses, "instructoremails", "A comma separated list of all primary and secondary instructor emails for the course.", typeof(string), "smith@tpro.ca, jones@tpro.ca")]
		COURSE_InstructorEmails,
		// Token: 0x04001229 RID: 4649
		[MailMergeCode(eMailMergeCodeGroup.Courses, "instructornames", "A comma separated list of all primary and secondary instructor emails for the course.", typeof(string), "John Smith, Mary Roberts")]
		COURSE_InstructorNames,
		// Token: 0x0400122A RID: 4650
		[MailMergeCode(eMailMergeCodeGroup.Courses, "instructornameswithemails", "A comma separated list of all primary and secondary instructor names and emails for the course.", typeof(string), "John Smith; email: john@tpro.ca, Mary Roberts; email: mary@tpro.ca")]
		COURSE_InstructorNamesWithEmails,
		// Token: 0x0400122B RID: 4651
		[MailMergeCode(eMailMergeCodeGroup.Courses, "coursecode", "The course code section of the course.", typeof(string), "101", new string[]
		{
			"course"
		})]
		COURSE_CourseCode,
		// Token: 0x0400122C RID: 4652
		[MailMergeCode(eMailMergeCodeGroup.Courses, "section", "The section of the course.", typeof(string), "2")]
		COURSE_Section,
		// Token: 0x0400122D RID: 4653
		[MailMergeCode(eMailMergeCodeGroup.Courses, "timeofday", "The time of day of the course; usually this indicates the type of class (eg. lab, lecture, tutorial).", typeof(string), "LEC")]
		COURSE_TimeOfDay,
		// Token: 0x0400122E RID: 4654
		[MailMergeCode(eMailMergeCodeGroup.Courses, "courses", "A comma separated list of course descriptions (see CourseDescription code)", typeof(string), "BIOL 101 2 LEC, CHEM 102 1 LAB")]
		COURSE_Courses,
		// Token: 0x0400122F RID: 4655
		[MailMergeCode(eMailMergeCodeGroup.Courses, "startyear", "The start year of the course.", typeof(DateTime), "2000")]
		COURSE_StartYear,
		// Token: 0x04001230 RID: 4656
		[MailMergeCode(eMailMergeCodeGroup.Courses, "startmonth", "The start month of the course.", typeof(DateTime), "January")]
		COURSE_StartMonth,
		// Token: 0x04001231 RID: 4657
		[MailMergeCode(eMailMergeCodeGroup.Courses, "coursestartdate", "The start date of the course.", typeof(DateTime), "January 2, 2000")]
		COURSE_CourseStartDate,
		// Token: 0x04001232 RID: 4658
		[MailMergeCode(eMailMergeCodeGroup.Courses, "courseenddate", "The end date of the course.", typeof(DateTime), "April 2, 2000")]
		COURSE_CourseEndDate,
		// Token: 0x04001233 RID: 4659
		[MailMergeCode(eMailMergeCodeGroup.Courses, "campus", "The course campus.", typeof(string), "MAIN")]
		COURSE_Campus,
		// Token: 0x04001234 RID: 4660
		[MailMergeCode(eMailMergeCodeGroup.Courses, "department", "The course department.", typeof(string), "Science")]
		COURSE_Department,
		// Token: 0x04001235 RID: 4661
		[MailMergeCode(eMailMergeCodeGroup.Courses, "lucourseid", "The internal ClockWork unique id of the course.  Note that different sections of the same course have different unique ids.", typeof(string), "2331", new string[]
		{
			"lucid"
		})]
		COURSE_LuCourseId,
		// Token: 0x04001236 RID: 4662
		[MailMergeCode(eMailMergeCodeGroup.Courses, "coursesession", "The title of the session the course is in based on the start date of the course.", typeof(string), "Summer 2012")]
		COURSE_Session,
		// Token: 0x04001237 RID: 4663
		[MailMergeCode(eMailMergeCodeGroup.Courses, "coursessignatures", "A list of courses with space for the instructor to sign or initial.", typeof(string), "")]
		COURSE_CoursesSignatures,
		// Token: 0x04001238 RID: 4664
		[MailMergeCode(eMailMergeCodeGroup.Courses, "coursessignaturesstudents", "A list of courses with space to sign or initial.", typeof(string), "")]
		COURSE_CoursesSignaturesStudents,
		// Token: 0x04001239 RID: 4665
		[MailMergeCode(eMailMergeCodeGroup.Courses, "coursetimetable", "A comma separated list of timetable entries for the course.", typeof(string), "Th 2:00 pm - 3:00 pm, Fr 9:00 am - 10:30 am")]
		COURSE_Timetable,
		// Token: 0x0400123A RID: 4666
		[MailMergeCode(eMailMergeCodeGroup.Courses, "coursetimetableandlocation", "A comma separated list of timetable entries (with locations) for the course.", typeof(string), "Th 2:00 pm - 3:00 pm [BCC 102a], Fr 9:00 am - 10:30 am [DET 332")]
		COURSE_TimetableWithLocation,
		// Token: 0x0400123B RID: 4667
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classdatetime", "The class date, start time, and end time, all in one line.", typeof(string), "January 1, 2000  3:00 pm to 4:00 pm")]
		EXAM_ClassDateTime,
		// Token: 0x0400123C RID: 4668
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classstartdatetime", "The class date and start time in one line.", typeof(string), "January 1, 2000  3:00 pm")]
		EXAM_ClassStartDateTime,
		// Token: 0x0400123D RID: 4669
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classenddatetime", "The class date and end time in one line.", typeof(string), "January 1, 2000  4:00 pm")]
		EXAM_ClassEndDateTime,
		// Token: 0x0400123E RID: 4670
		[MailMergeCode(eMailMergeCodeGroup.Exams, "examid", "The internal ClockWork exam id, which is unique to the class test.", typeof(int), "2343")]
		EXAM_ExamId,
		// Token: 0x0400123F RID: 4671
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classdate", "The date the class is writing.", typeof(DateTime), "Monday January 23, 2000")]
		EXAM_ClassDate,
		// Token: 0x04001240 RID: 4672
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classdate2", "The date the class is writing; format 2", typeof(DateTime), "01/23/00")]
		EXAM_ClassDate2,
		// Token: 0x04001241 RID: 4673
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classdate3", "The date the class is writing; format 3", typeof(DateTime), "2000-01-23")]
		EXAM_ClassDate3,
		// Token: 0x04001242 RID: 4674
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classstarttime", "The start time for the class.", typeof(DateTime), "3:00 pm")]
		EXAM_ClassStartTime,
		// Token: 0x04001243 RID: 4675
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classendtime", "The end time for the class", typeof(DateTime), "4:00 pm")]
		EXAM_ClassEndTime,
		// Token: 0x04001244 RID: 4676
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classduration", "The length of time the class is writing", typeof(string), "1 hour and 23 minutes")]
		EXAM_ClassDuration,
		// Token: 0x04001245 RID: 4677
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classdurationminutes", "The number of minutes the class is writing", typeof(int), "90")]
		EXAM_ClassDurationMinutes,
		// Token: 0x04001246 RID: 4678
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classlocation", "The location of the class test.  This is usually only available for final exams.", typeof(string), "MMSC B123")]
		EXAM_ClassLocation,
		// Token: 0x04001247 RID: 4679
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classtypecode", "Final exam or midterm", typeof(string), "Final exam")]
		EXAM_ClassTypeCode,
		// Token: 0x04001248 RID: 4680
		[MailMergeCode(eMailMergeCodeGroup.Exams, "testdelivered", "The 'test delivered' status of the class test.", typeof(string), "Delivered online")]
		EXAM_TestDelivered,
		// Token: 0x04001249 RID: 4681
		[MailMergeCode(eMailMergeCodeGroup.Exams, "instructoracknowledged", "Instructor acknowledged test status.", typeof(string), "")]
		EXAM_InstructorAcknowledged,
		// Token: 0x0400124A RID: 4682
		[MailMergeCode(eMailMergeCodeGroup.Exams, "instructorcontacteddate", "The date the instructor was contacted", typeof(string), "2000-01-23")]
		EXAM_InstructorContactedDate,
		// Token: 0x0400124B RID: 4683
		[MailMergeCode(eMailMergeCodeGroup.Exams, "instructorcontactednote", "The note entered for the instructor was contacted info.", typeof(string), "")]
		EXAM_InstructorContactedNote,
		// Token: 0x0400124C RID: 4684
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classprivatenote", "The class test private note.", typeof(string), "")]
		EXAM_ClassPrivateNote,
		// Token: 0x0400124D RID: 4685
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classtestpickedupnote", "The class test picked up note.", typeof(string), "")]
		EXAM_ClassTestPickedUpNote,
		// Token: 0x0400124E RID: 4686
		[MailMergeCode(eMailMergeCodeGroup.Exams, "classtestpickedupdate", "The class test picked up date.", typeof(DateTime), "2000-01-23")]
		EXAM_ClassTestPickedUpDate,
		// Token: 0x0400124F RID: 4687
		[MailMergeCode(eMailMergeCodeGroup.Exams, "instructorform", "A bulleted list of all entered fields on the instructor class test form.", typeof(IList<DynamicData>), "* Crib sheets allowed\r\n* Location of test: MSSS 323")]
		EXAM_InstructorForm,
		// Token: 0x04001250 RID: 4688
		[MailMergeCode(eMailMergeCodeGroup.Student, "instructorexamurl", "The url for the instructor to update the class test definition information online.", typeof(string), "https://www.tpro.ca/clockwork/user/instructor/examupload.aspx?examid=sdlkdsf&lucid=lsdfjldskfj")]
		EXAM_InstructorUrl,
		// Token: 0x04001251 RID: 4689
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "proctor", "Displays the word 'Proctor' if in U.S. mode, or the word 'Invigilator' otherwise.  Note: this does not display the actual proctor/invigilator name but just the term.  Use the code 'proctorname' for the actual name of the proctor/invigilator.", typeof(string), "")]
		BASECODES_Proctor,
		// Token: 0x04001252 RID: 4690
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "date", "The current date.", typeof(DateTime), "January 23, 2000")]
		BASECODES_Date,
		// Token: 0x04001253 RID: 4691
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "academic year", "The current school year range.", typeof(string), "2000 - 2001", new string[]
		{
			"academic_year"
		})]
		BASECODES_Academic_Year,
		// Token: 0x04001254 RID: 4692
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "semester", "The first two letters of the current semester.", typeof(string), "WI")]
		BASECODES_Semester,
		// Token: 0x04001255 RID: 4693
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "academicterm", "The description of the current semester.", typeof(string), "Winter", new string[]
		{
			"semester2",
			"semester3"
		})]
		BASECODES_AcademicTerm,
		// Token: 0x04001256 RID: 4694
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "time", "The current time.", typeof(DateTime), "2:30 pm")]
		BASECODES_Time,
		// Token: 0x04001257 RID: 4695
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "timemilitary", "The current time in military/railway time format.", typeof(DateTime), "14:30", new string[]
		{
			"datemilitary"
		})]
		BASECODES_TimeMilitary,
		// Token: 0x04001258 RID: 4696
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "activeuser", "The first and last name of the user who is logged in.", typeof(string), "Michael Smith")]
		BASECODES_ActiveUser,
		// Token: 0x04001259 RID: 4697
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "activeuserfirstname", "The first name of the user who is logged in.", typeof(string), "Michael")]
		BASECODES_ActiveUserFirstName,
		// Token: 0x0400125A RID: 4698
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "activeuserlastname", "The last name of the user who is logged in.", typeof(string), "Smith")]
		BASECODES_ActiveUserLastName,
		// Token: 0x0400125B RID: 4699
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "activeuseremail", "The email of the user who is logged in.", typeof(string), "mike_smith@tpro.ca")]
		BASECODES_ActiveUserEmail,
		// Token: 0x0400125C RID: 4700
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "activeuserphone", "The phone number of the user who is logged in.", typeof(string), "123-555-1234")]
		BASECODES_ActiveUserPhone,
		// Token: 0x0400125D RID: 4701
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "activeusertitle", "The title of the user who is logged in.", typeof(string), "Advisor")]
		BASECODES_ActiveUserTitle,
		// Token: 0x0400125E RID: 4702
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "activeusercontact", "The contact information (email and phone) of the user who is logged in.", typeof(string), "123-555-3322 (phone) or bob@tpro.ca (email)")]
		BASECODES_ActiveUserContact,
		// Token: 0x0400125F RID: 4703
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "activeusersignature", "The signature of the user who is logged in.", typeof(DateTime), "[image] - not available for email templates")]
		BASECODES_ActiveUserSignature,
		// Token: 0x04001260 RID: 4704
		[MailMergeCode(eMailMergeCodeGroup.BaseCodes, "clockwork", "This is a code for testing purposes only.  It will always return the IP address of the computer it is generated on.", typeof(string), "127.0.0.1")]
		BASECODES_ClockWork,
		// Token: 0x04001261 RID: 4705
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodations", "All accommodations in bullet format that are marked as classroom, test/exam, other, or report.", typeof(string), "* Extra time: 20%\r\n* Private alone room")]
		ACCOMMODATIONS_Accommodations,
		// Token: 0x04001262 RID: 4706
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsline", "All accommodations in comma separated format that are marked as classroom, test/exam, other, or report.", typeof(string), "Extra time: 20%, Private alone room")]
		ACCOMMODATIONS_AccommodationsLine,
		// Token: 0x04001263 RID: 4707
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsfr", "Alternate language captions for all accommodations (in bullet format) that are marked as classroom, test/exam, other, or report.", typeof(string), "* Extra time: 20%\r\n* Private alone room")]
		ACCOMMODATIONS_AccommodationsFr,
		// Token: 0x04001264 RID: 4708
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsprof", "Classroom related accommodations in bullet format that are marked as classroom, test/exam, other, or report.", typeof(string), "* Additional time to complete assignments\r\n* Notetaker")]
		ACCOMMODATIONS_AccommodationsProf,
		// Token: 0x04001265 RID: 4709
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsexam", "Test/exam related accommodations in bullet format that are marked as classroom, test/exam, other, or report.", typeof(string), "* Extra time: 20%\r\n* Private alone room")]
		ACCOMMODATIONS_AccommodationsExam,
		// Token: 0x04001266 RID: 4710
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsother", "'Other' related accommodations in bullet format that are marked as classroom, test/exam, other, or report.", typeof(string), "* Extra time: 20%\r\n* Private alone room")]
		ACCOMMODATIONS_AccommodationsOther,
		// Token: 0x04001267 RID: 4711
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsreport", "'Report' related accommodations in bullet format that are marked as classroom, test/exam, other, or report.", typeof(string), "* Extra time: 20%\r\n* Private alone room")]
		ACCOMMODATIONS_AccommodationsReport,
		// Token: 0x04001268 RID: 4712
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsapproved", "All accommodations that have been approved in bullet format.  Only active if accommodations approval process is activated in ClockWork settings.", typeof(string), "* Extra time: 20%\r\n* Private alone room")]
		ACCOMMODATIONS_AccommodationsApproved,
		// Token: 0x04001269 RID: 4713
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsapprovedprof", "Classroom related accommodations that have been approved in bullet format.  Only active if accommodations approval process is activated in ClockWork settings.", typeof(string), "* Extra time: 20%\r\n* Private alone room")]
		ACCOMMODATIONS_AccommodationsApprovedProf,
		// Token: 0x0400126A RID: 4714
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsapprovedexam", "Test/exam related accommodations that have been approved in bullet format.  Only active if accommodations approval process is activated in ClockWork settings.", typeof(string), "* Extra time: 20%\r\n* Private alone room")]
		ACCOMMODATIONS_AccommodationsApprovedExam,
		// Token: 0x0400126B RID: 4715
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsapprovedother", "'Other' related accommodations that have been approved in bullet format.  Only active if accommodations approval process is activated in ClockWork settings.", typeof(string), "* Extra time: 20%\r\n* Private alone room")]
		ACCOMMODATIONS_AccommodationsApprovedOther,
		// Token: 0x0400126C RID: 4716
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsapprovedother", "'Report' related accommodations that have been approved in bullet format.  Only active if accommodations approval process is activated in ClockWork settings.", typeof(string), "* Extra time: 20%\r\n* Private alone room")]
		ACCOMMODATIONS_AccommodationsApprovedReport,
		// Token: 0x0400126D RID: 4717
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationcount", "The number of accommodations for the student.", typeof(int), "4", new string[]
		{
			"accommodationscount"
		})]
		ACCOMMODATIONS_AccommodationCount,
		// Token: 0x0400126E RID: 4718
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsshortcode", "Same as 'accommodations' code, but you provide a shortcode(s) to filter the list by using: 'shortcodes=abc.def' in the mail merge code.", typeof(string), "* Extra time: 20%\r\n* Private alone room")]
		Accommodations_AccommodationsShortCode,
		// Token: 0x0400126F RID: 4719
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "dateletterissued", "The date the accommodation letter was last generated for this course.", typeof(DateTime), "January 4, 2012")]
		Accommodations_DateLetterIssued,
		// Token: 0x04001270 RID: 4720
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "datestudentlastviewedletter", "The date the accommodation letter was last viewed by the student for this course.", typeof(DateTime), "January 4, 2012")]
		Accommodations_DateStudentLastViewedLetter,
		// Token: 0x04001271 RID: 4721
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "dateletterreturned", "The date the accommodation letter was last confirmed viewed by the instructor for this course.", typeof(DateTime), "January 4, 2012")]
		Accommodations_DateLetterReturned,
		// Token: 0x04001272 RID: 4722
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "examaccommodationsshort", "A list of (exam only) accommodation short codes (AccommodationGroup property in the form builder tool in the ClockWork Admin).  If no short code is available then the full title will be used.", typeof(string), "24HOURS,EXTRATIME,PRIVATEROOM")]
		Accommodations_ExamAccommodationsShort,
		// Token: 0x04001273 RID: 4723
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "profaccommodationsshort", "A list of (classroom only) accommodation short codes (AccommodationGroup property in the form builder tool in the ClockWork Admin).  If no short code is available then the full title will be used.", typeof(string), "ASSIGNMENTEXT,LENIENCYSPELL")]
		Accommodations_ProfAccommodationsShort,
		// Token: 0x04001274 RID: 4724
		[MailMergeCode(eMailMergeCodeGroup.Accommodations, "accommodationsinsert", "Allows inserting of another mail merge document if a checkbox is checked on the accommodations template.  Note that the inserted mail merge document cannot contain additional mail-merge codes; it is a static document insert only. Example: #<accommodationsinsert`tid=116`cid=7350>#", typeof(string), "")]
		Accommodations_AccommodationsInsert,
		// Token: 0x04001275 RID: 4725
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "subtitle", "The appointment subtitle.", typeof(string), "Visit from Sarah", new string[]
		{
			"appsubtitle"
		})]
		APPOINTMENTS_SUBTITLE,
		// Token: 0x04001276 RID: 4726
		[MailMergeCode(eMailMergeCodeGroup.Student, "personid", "The internal ClockWork id of the student.", typeof(string), "332", new string[]
		{
			"pid"
		})]
		STUDENT_PersonId,
		// Token: 0x04001277 RID: 4727
		[MailMergeCode(eMailMergeCodeGroup.Student, "firstname", "The student's first name.", typeof(string), "Michelle", new string[]
		{
			"studentfirstname"
		})]
		STUDENT_FirstName,
		// Token: 0x04001278 RID: 4728
		[MailMergeCode(eMailMergeCodeGroup.Student, "preferredname", "The student's preferred first name - requires controlid in parameters or preferred first name field to have 'preferredname' as 'name' field in form builder.", typeof(string), "Michelle", new string[]
		{
			"preferredfirstname"
		})]
		STUDENT_PreferredFirstName,
		// Token: 0x04001279 RID: 4729
		[MailMergeCode(eMailMergeCodeGroup.Student, "middlename", "The student's middle name", typeof(string), "Lee", new string[]
		{
			"studentmiddlename"
		})]
		STUDENT_MiddleName,
		// Token: 0x0400127A RID: 4730
		[MailMergeCode(eMailMergeCodeGroup.Student, "lastname", "The student's last name.", typeof(string), "Smith", new string[]
		{
			"studentlastname"
		})]
		STUDENT_LastName,
		// Token: 0x0400127B RID: 4731
		[MailMergeCode(eMailMergeCodeGroup.Student, "initials", "The student's first, middle and last initials.", typeof(string), "A.M.D.", new string[]
		{
			"studentinitials"
		})]
		STUDENT_Initials,
		// Token: 0x0400127C RID: 4732
		[MailMergeCode(eMailMergeCodeGroup.Student, "student_no", "The student's student number.", typeof(string), "9933221", new string[]
		{
			"studentno"
		})]
		STUDENT_Student_no,
		// Token: 0x0400127D RID: 4733
		[MailMergeCode(eMailMergeCodeGroup.Student, "age", "The current age of the student (based on birth date field)", typeof(int), "19")]
		STUDENT_Age,
		// Token: 0x0400127E RID: 4734
		[MailMergeCode(eMailMergeCodeGroup.Student, "studentemail", "The student's email address.", typeof(string), "msmith@tpro.ca", new string[]
		{
			"email"
		})]
		STUDENT_StudentEmail,
		// Token: 0x0400127F RID: 4735
		[MailMergeCode(eMailMergeCodeGroup.Student, "studentphone", "The student's phone number.", typeof(string), "123-555-2231")]
		STUDENT_StudentPhone,
		// Token: 0x04001280 RID: 4736
		[MailMergeCode(eMailMergeCodeGroup.Student, "accommodationsexpiry", "The accommodations expiry date for the student (only works if expiry date is enabled in the ClockWork settings)", typeof(string), "January 22, 2000")]
		STUDENT_AccommodationsExpiry,
		// Token: 0x04001281 RID: 4737
		[MailMergeCode(eMailMergeCodeGroup.Student, "heshe", "Provides 'He' if the student is male, 'She' if female, 'He/she' if unknown.", typeof(string), "He", IsHidden = true)]
		STUDENT_HeShe,
		// Token: 0x04001282 RID: 4738
		[MailMergeCode(eMailMergeCodeGroup.Student, "_heshe", "Lower case version of 'heshe' code.  Provides 'he' if the student is male, 'she' if female, 'he/she' if unknown.", typeof(string), "", new string[]
		{
			"heshelower"
		}, IsHidden = true)]
		STUDENT_HeSheLower,
		// Token: 0x04001283 RID: 4739
		[MailMergeCode(eMailMergeCodeGroup.Student, "counsellor", "The full name of the student's assigned advisor.", typeof(string), "Jane Smith", new string[]
		{
			"counsellorname",
			"advisor",
			"advisorname",
			"counselor",
			"counsellorname"
		})]
		STUDENT_Counsellor,
		// Token: 0x04001284 RID: 4740
		[MailMergeCode(eMailMergeCodeGroup.Student, "counsellorfirstname", "The first name of the student's assigned advisor.", typeof(string), "Jane", new string[]
		{
			"counselorfirstname",
			"advisorfirstname"
		})]
		STUDENT_CounsellorFirstName,
		// Token: 0x04001285 RID: 4741
		[MailMergeCode(eMailMergeCodeGroup.Student, "counsellorlastname", "The last name of the student's assigned advisor.", typeof(string), "Smith", new string[]
		{
			"counselorlastname",
			"advisorlastname"
		})]
		STUDENT_CounsellorLastName,
		// Token: 0x04001286 RID: 4742
		[MailMergeCode(eMailMergeCodeGroup.Student, "counselloremail", "The email address of the student's assigned advisor.", typeof(string), "janesmith@tpro.ca", new string[]
		{
			"counseloremail",
			"advisoremail"
		})]
		STUDENT_CounsellorEmail,
		// Token: 0x04001287 RID: 4743
		[MailMergeCode(eMailMergeCodeGroup.Student, "counsellorphone", "The phone number of the student's assigned advisor.", typeof(string), "123-555-3233", new string[]
		{
			"counsellorworkphone",
			"counsellorhomephone",
			"counselorphone",
			"counselorworkphone",
			"counselorhomephone",
			"advisorphone",
			"advisorworkphone",
			"advisorhomephone"
		})]
		STUDENT_CounsellorPhone,
		// Token: 0x04001288 RID: 4744
		[MailMergeCode(eMailMergeCodeGroup.Student, "counsellortitle", "The title of the student's assigned advisor.", typeof(string), "Disability Advisor", new string[]
		{
			"counselortitle",
			"advisortitle"
		})]
		STUDENT_CounsellorTitle,
		// Token: 0x04001289 RID: 4745
		[MailMergeCode(eMailMergeCodeGroup.Student, "counsellorcontact", "The contact information (email and phone) of the student's assigned advisor.", typeof(string), "123-555-3322 (phone) or bob@tpro.ca (email)", new string[]
		{
			"counselorcontact",
			"advisorcontact"
		})]
		STUDENT_CounsellorContact,
		// Token: 0x0400128A RID: 4746
		[MailMergeCode(eMailMergeCodeGroup.Student, "counsellorsignature", "The signature image of the student's assigned advisor.  This code will lookup the student's assigned advisor, and then use the picture or file control that exists on the staff info form.  The advisor signature control id is stored in a setting; this setting has to be configured in order for this to work.  To use the signature that appears on the Accommodation Generate Letter popup, use the #<staffsignature># mail merge code instead.", typeof(string), "[image] - not available for email templates", new string[]
		{
			"counselorsignature",
			"advisorsignature"
		})]
		STUDENT_CounsellorSignature,
		// Token: 0x0400128B RID: 4747
		[MailMergeCode(eMailMergeCodeGroup.Student, "dateadded", "The date the student was added into ClockWork.", typeof(string), "January 5, 2000")]
		STUDENT_DateAdded,
		// Token: 0x0400128C RID: 4748
		[MailMergeCode(eMailMergeCodeGroup.Student, "instructorurl", "The url for the instructor to get the student's accommodation letter for the current course context.", typeof(string), "http://www.tpro.ca/clockwork/user/instructor/iletter?pid=sdlkdsf&lucid=lsdfjldskfj")]
		STUDENT_InstructorUrl,
		// Token: 0x0400128D RID: 4749
		[MailMergeCode(eMailMergeCodeGroup.Student, "studenturl", "The url for the student to get their own accommodation letter for the current course context.", typeof(string), "http://www.tpro.ca/clockwork/user/test/iletter?pid=sdlkdsf&lucid=lsdfjldskfj")]
		STUDENT_StudentUrl,
		// Token: 0x0400128E RID: 4750
		[MailMergeCode(eMailMergeCodeGroup.Student, "studentsignature", "The student's accommodation signature image.", typeof(string), "[image] - not available for email templates")]
		STUDENT_StudentSignature,
		// Token: 0x0400128F RID: 4751
		[MailMergeCode(eMailMergeCodeGroup.Student, "staffsignature", "The staff signature on the student's accommodations image.  This appears when you click 'Generate letter' on the accommodations form.", typeof(string), "[image] - not available for email templates")]
		STUDENT_StaffSignature,
		// Token: 0x04001290 RID: 4752
		[MailMergeCode(eMailMergeCodeGroup.Tests, "scheduledendtimewithoutbreaks", "The scheduled end time without break time.", typeof(DateTime), "3:35 pm")]
		TESTS_ScheduledEndTimeWithoutBreaks,
		// Token: 0x04001291 RID: 4753
		[MailMergeCode(eMailMergeCodeGroup.Tests, "scheduleddurationwithoutbreaks", "The scheduled duration without break time.", typeof(string), "1 hour and 45 minutes")]
		TESTS_ScheduledDurationWithoutBreaks,
		// Token: 0x04001292 RID: 4754
		[MailMergeCode(eMailMergeCodeGroup.Tests, "breakduration", "The total break time allowed.", typeof(string), "1 hour and 45 minutes")]
		TESTS_BreakDuration,
		// Token: 0x04001293 RID: 4755
		[MailMergeCode(eMailMergeCodeGroup.Tests, "actualdate", "The actual date the student started their test.", typeof(DateTime), "January 2, 2000", IsHidden = true)]
		TESTS_ActualDate,
		// Token: 0x04001294 RID: 4756
		[MailMergeCode(eMailMergeCodeGroup.Tests, "actualstarttime", "The actual time the student started their test.", typeof(DateTime), "3:45 pm")]
		TESTS_ActualStartTime,
		// Token: 0x04001295 RID: 4757
		[MailMergeCode(eMailMergeCodeGroup.Tests, "actualendtime", "The actual time the student completed their test.", typeof(DateTime), "4:50 pm")]
		TESTS_ActualEndTime,
		// Token: 0x04001296 RID: 4758
		[MailMergeCode(eMailMergeCodeGroup.Tests, "actualduration", "The actual duration the student took to complete their test.", typeof(string), "1 hour and 45 minutes")]
		TESTS_ActualDuration,
		// Token: 0x04001297 RID: 4759
		[MailMergeCode(eMailMergeCodeGroup.Tests, "actualdurationminutes", "The actual number of minutes the student took to complete their test.", typeof(int), "95")]
		TESTS_ActualDurationMinutes,
		// Token: 0x04001298 RID: 4760
		[MailMergeCode(eMailMergeCodeGroup.Tests, "examaccommodations", "A bulleted list of exam accommodations that are marked as required for this test.  This will not show all exam accommodations, only exam accommodations in use for this test.  Control caption is always used, as opposed to long description if available on the accommodationsexam mail merge code, which would show all of the student's eligible exam accommodations.", typeof(string), "* Private room\r\n* Extra time: 50%")]
		TESTS_ExamAccommodations,
		// Token: 0x04001299 RID: 4761
		[MailMergeCode(eMailMergeCodeGroup.Tests, "bookingnotes", "The computer generated booking notes for the test or exam booking.", typeof(string), "Added 30 minutes extra time due to extra time accommodation")]
		TESTS_BookingNotes,
		// Token: 0x0400129A RID: 4762
		[MailMergeCode(eMailMergeCodeGroup.Tests, "privatenotes", "The private notes for the test or exam booking.", typeof(string), "Student arrived late due to transportation problem")]
		TESTS_PrivateNotes,
		// Token: 0x0400129B RID: 4763
		[MailMergeCode(eMailMergeCodeGroup.Tests, "examreqaccommodationsshort", "A comma separated list of exam accommodations that are marked as required for this test.  The 'Accommodation group' value will be used if filled in (this can be found in the form builder in the ClockWork admin in the properties list for each field).", typeof(string), "* PRIVATE\r\n* XTRATIME: 50%")]
		TESTS_ExamAccommodationsShort,
		// Token: 0x0400129C RID: 4764
		[MailMergeCode(eMailMergeCodeGroup.Tests, "examstatus", "The exam status (on the tests/exams screen you can right-click and choose the exam status)", typeof(string), "Hold")]
		TESTS_ExamStatus,
		// Token: 0x0400129D RID: 4765
		[MailMergeCode(eMailMergeCodeGroup.Tests, "proctorname", "The name of the proctor/invigilator assigned to the test/exam.", typeof(string), "Joe Smith", new string[]
		{
			"invigilatorname"
		})]
		TESTS_ProctorName,
		// Token: 0x0400129E RID: 4766
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "appointmentid", "The internal ClockWork id for the test or appointment or workshop.", typeof(DateTime), "2331", new string[]
		{
			"appid"
		})]
		APPOINTMENTS_AppointmentId,
		// Token: 0x0400129F RID: 4767
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "appdate", "The date of the test or appointment or workshop.", typeof(DateTime), "Monday January 20, 2000", new string[]
		{
			"appstartdate",
			"scheduleddate"
		})]
		APPOINTMENTS_AppDate,
		// Token: 0x040012A0 RID: 4768
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "scheduleddate2", "Same as scheduleddate but returns date in a different format.", typeof(DateTime), "01/20/00")]
		APPOINTMENTS_ScheduledDate2,
		// Token: 0x040012A1 RID: 4769
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "scheduleddate3", "Same as scheduleddate but returns date in a different format.", typeof(DateTime), "2000-01-20")]
		APPOINTMENTS_ScheduledDate3,
		// Token: 0x040012A2 RID: 4770
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "appstarttime", "The start time of the test or appointment or workshop.", typeof(DateTime), "2:30 pm", new string[]
		{
			"appstartdatetime",
			"scheduledstarttime",
			"starttime"
		})]
		APPOINTMENTS_AppStartTime,
		// Token: 0x040012A3 RID: 4771
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "appendtime", "The end time of the test or appointment or workshop.", typeof(DateTime), "3:30 pm", new string[]
		{
			"appenddatetime",
			"scheduledendtime",
			"endtime"
		})]
		APPOINTMENTS_AppEndTime,
		// Token: 0x040012A4 RID: 4772
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "scheduledstartdatetime", "The date and start time of the test or appointment or workshop.", typeof(string), "January 20, 2000  2:30 pm")]
		APPOINTMENTS_ScheduledStartDateTime,
		// Token: 0x040012A5 RID: 4773
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "scheduledenddatetime", "The date and end time of the test or appointment or workshop.", typeof(string), "January 20, 2000  3:30 pm")]
		APPOINTMENTS_ScheduledEndDateTime,
		// Token: 0x040012A6 RID: 4774
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "appstartdatedayofweek", "The day of week of the test or appointment or workshop.", typeof(DateTime), "Monday")]
		APPOINTMENTS_AppStartDateDayOfWeek,
		// Token: 0x040012A7 RID: 4775
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "appdurationminutes", "The number of minutes the test or appointment or workshop is scheduled.", typeof(int), "90", new string[]
		{
			"scheduleddurationminutes",
			"durationminutes"
		})]
		APPOINTMENTS_AppDurationMinutes,
		// Token: 0x040012A8 RID: 4776
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "appduration", "The duration of the test or appointment or workshop.", typeof(string), "1 hour and 30 minutes", new string[]
		{
			"scheduledduration",
			"duration"
		})]
		APPOINTMENTS_AppDuration,
		// Token: 0x040012A9 RID: 4777
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "apptime", "The start and end time of the test or appointment or workshop.", typeof(string), "2:30 pm to 3:30 pm")]
		APPOINTMENTS_AppTime,
		// Token: 0x040012AA RID: 4778
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "memo", "The memo of the test or appointment or workshop.", typeof(string), "Notes entered into the memo area in plain text.", new string[]
		{
			"appmemo",
			"appointmentmemo"
		})]
		APPOINTMENTS_Memo,
		// Token: 0x040012AB RID: 4779
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "appdescription", "The appointment title.", typeof(string), "Advising appointment", new string[]
		{
			"apptypedescription",
			"apptitle"
		})]
		APPOINTMENTS_AppDescription,
		// Token: 0x040012AC RID: 4780
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "apptypeid", "The internal ClockWork id of the appointment type (title).", typeof(int), "32")]
		APPOINTMENTS_AppTypeId,
		// Token: 0x040012AD RID: 4781
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "appcode", "The appointment appcode id.", typeof(int), "21", new string[]
		{
			"appcodedescription"
		})]
		APPOINTMENTS_AppCode,
		// Token: 0x040012AE RID: 4782
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "appcodeid", "The appointment appcode id.", typeof(string), "", IsHidden = true)]
		APPOINTMENTS_AppCodeId,
		// Token: 0x040012AF RID: 4783
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "roompid", "The internal ClockWork id of the assigned room.", typeof(int), "23", new string[]
		{
			"roompersonid"
		})]
		APPOINTMENTS_RoomPid,
		// Token: 0x040012B0 RID: 4784
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "room", "The assigned room.", typeof(string), "MIST B102", new string[]
		{
			"roomdescription",
			"approom"
		})]
		APPOINTMENTS_Room,
		// Token: 0x040012B1 RID: 4785
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "roomdescriptionfirstword", "The first word (before the first space) of the assigned room.", typeof(string), "MIST")]
		APPOINTMENTS_RoomDescriptionFirstWord,
		// Token: 0x040012B2 RID: 4786
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "roomdescriptionlastword", "The last word (after the last space) of the assigned room.", typeof(string), "B102")]
		APPOINTMENTS_RoomDescriptionLastWord,
		// Token: 0x040012B3 RID: 4787
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "location", "The text field 'location' of the test or appointment or workshop.  This is the 'Alternate location' for tests and exams.", typeof(string), "John's office")]
		APPOINTMENTS_Location,
		// Token: 0x040012B4 RID: 4788
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "roomandlocation", "The room and location descriptions as one string.", typeof(string), "MIST B102 John's office")]
		APPOINTMENTS_RoomAndLocation,
		// Token: 0x040012B5 RID: 4789
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "cancelled", "Is the test or appointment or workshop cancelled?", typeof(bool), "Yes")]
		APPOINTMENTS_Cancelled,
		// Token: 0x040012B6 RID: 4790
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "attendeesemailsstudents", "A comma separated list of all student attendee emails.", typeof(string), "bob@tpro.ca,sally@tpro.ca", new string[]
		{
			"attendeesemails",
			"studentemails",
			"studentsemails"
		})]
		APPOINTMENTS_AttendeesEmailsStudents,
		// Token: 0x040012B7 RID: 4791
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "attendeesemailsall", "A comma separated list of all attendee emails.  Filter by a specific group using the parameter code gid.", typeof(string), "bob@tpro.ca,sally@tpro.ca")]
		APPOINTMENTS_AttendeesEmailsAll,
		// Token: 0x040012B8 RID: 4792
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "attendeesemailsstaff", "A comma separated list of all staff attendee emails.", typeof(string), "bob@tpro.ca,sally@tpro.ca", new string[]
		{
			"staffemails"
		})]
		APPOINTMENTS_AttendeesEmailsStaff,
		// Token: 0x040012B9 RID: 4793
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "attendees", "A comma separated list of all attendee names.  Filter by a specific group using the parameter code gid.", typeof(string), "John Smith, Mary Adams, Michele Roberts")]
		APPOINTMENTS_Attendees,
		// Token: 0x040012BA RID: 4794
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "attendeescount", "Total number of attendees.  Filter by a specific group using the parameter code gid.", typeof(int), "3")]
		APPOINTMENTS_AttendeesCount,
		// Token: 0x040012BB RID: 4795
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "attendeesnonstudents", "A comma separated list of all attendee names who are not students.", typeof(string), "John Smith, Mary Adams")]
		APPOINTMENTS_AttendeesNonStudents,
		// Token: 0x040012BC RID: 4796
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "attendeesstudents", "A comma separated list of only student attendee names and student numbers.", typeof(string), "Roberts, Michele . 1223321")]
		APPOINTMENTS_AttendeesStudents,
		// Token: 0x040012BD RID: 4797
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "attendeesmarknoshows", "A comman separated list of attendee names with no-show status beside each name.  Filter by a specific group using the parameter code gid.", typeof(string), "", IsHidden = true)]
		APPOINTMENTS_AttendeesMarkNoShows,
		// Token: 0x040012BE RID: 4798
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "attendeesonlystaff", "A comma separated list of all staff attendee names", typeof(string), "John Smith, Mary Adams")]
		APPOINTMENTS_AttendeesOnlyStaff,
		// Token: 0x040012BF RID: 4799
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "attendeesnofacilitatorsstaff", "A comma separated list of all attendee names for attendees who are not staff or facilitators", typeof(string), "", IsHidden = true)]
		APPOINTMENTS_AttendeesNoFacilitatorsStaff,
		// Token: 0x040012C0 RID: 4800
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "workshop", "The title of the workshop.", typeof(string), "Study skills and time management")]
		APPOINTMENTS_Workshop,
		// Token: 0x040012C1 RID: 4801
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "datebooked", "The date the appointment was originally booked.", typeof(DateTime), "2012-01-01", new string[]
		{
			"dateadded",
			"bookeddate",
			"addeddate"
		})]
		APPOINTMENTS_DateBooked,
		// Token: 0x040012C2 RID: 4802
		[MailMergeCode(eMailMergeCodeGroup.Appointments, "whobooked", "Who originally booked the appointment.", typeof(string), "John Smith")]
		APPOINTMENTS_WhoBooked,
		// Token: 0x040012C3 RID: 4803
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productuniqueid", "Inventory product unique id", typeof(string), "")]
		InventoryProducts_ProductUniqueId,
		// Token: 0x040012C4 RID: 4804
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productid", "Inventory product id", typeof(int), "")]
		InventoryProducts_ProductId,
		// Token: 0x040012C5 RID: 4805
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productname", "Inventory product name", typeof(string), "")]
		InventoryProducts_ProductName,
		// Token: 0x040012C6 RID: 4806
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productcategory", "Inventory product category", typeof(string), "")]
		InventoryProducts_ProductCategory,
		// Token: 0x040012C7 RID: 4807
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productserialnumber", "Inventory product serial number", typeof(string), "")]
		InventoryProducts_ProductSerialNumber,
		// Token: 0x040012C8 RID: 4808
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productbarcode", "Inventory product barcode", typeof(string), "")]
		InventoryProducts_ProductBarcode,
		// Token: 0x040012C9 RID: 4809
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productbarcodeimage", "Inventory product barcode image", typeof(string), "")]
		InventoryProducts_ProductBarcodeImage,
		// Token: 0x040012CA RID: 4810
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productstatus", "Inventory product status", typeof(string), "")]
		InventoryProducts_ProductStatus,
		// Token: 0x040012CB RID: 4811
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productowner", "Inventory product owner", typeof(string), "")]
		InventoryProducts_ProductOwner,
		// Token: 0x040012CC RID: 4812
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productdescription", "Inventory product description", typeof(string), "")]
		InventoryProducts_ProductDescription,
		// Token: 0x040012CD RID: 4813
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productnotes", "Inventory product notes", typeof(string), "")]
		InventoryProducts_ProductNotes,
		// Token: 0x040012CE RID: 4814
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productgroup", "Inventory product group", typeof(string), "")]
		InventoryProducts_ProductGroup,
		// Token: 0x040012CF RID: 4815
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productisloaned", "Is product loaned?", typeof(bool), "")]
		InventoryProducts_ProductIsLoaned,
		// Token: 0x040012D0 RID: 4816
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productlocationcampus", "Inventory product location campus", typeof(string), "")]
		InventoryProducts_ProductLocationCampus,
		// Token: 0x040012D1 RID: 4817
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productlocationbuilding", "Inventory product location building", typeof(string), "")]
		InventoryProducts_ProductLocationBuilding,
		// Token: 0x040012D2 RID: 4818
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productlocationroom", "Inventory product location room", typeof(string), "")]
		InventoryProducts_ProductLocationRoom,
		// Token: 0x040012D3 RID: 4819
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productlocationseat", "Inventory product location seat", typeof(string), "")]
		InventoryProducts_ProductLocationSeat,
		// Token: 0x040012D4 RID: 4820
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productlocationnotes", "Inventory product location notes", typeof(string), "")]
		InventoryProducts_ProductLocationNotes,
		// Token: 0x040012D5 RID: 4821
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productlocation", "Inventory product location", typeof(string), "")]
		InventoryProducts_ProductLocation,
		// Token: 0x040012D6 RID: 4822
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productvendorname", "Inventory product vendor name", typeof(string), "")]
		InventoryProducts_ProductVendorName,
		// Token: 0x040012D7 RID: 4823
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productvendorpurchasedate", "Inventory product purchase date", typeof(DateTime), "")]
		InventoryProducts_ProductVendorPurchaseDate,
		// Token: 0x040012D8 RID: 4824
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productvendorpurchaseamount", "Inventory product purchase amount", typeof(double), "")]
		InventoryProducts_ProductVendorPurchaseAmount,
		// Token: 0x040012D9 RID: 4825
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productvendorwarrantyexpiration", "Inventory product warranty expiration date", typeof(DateTime), "")]
		InventoryProducts_ProductVendorWarrantyExpiration,
		// Token: 0x040012DA RID: 4826
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productvendorpurchaseinfo", "Inventory product purchase info", typeof(string), "")]
		InventoryProducts_ProductVendorPurchaseInfo,
		// Token: 0x040012DB RID: 4827
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productthumbnail", "Inventory product thumbnail picture", typeof(string), "")]
		InventoryProducts_ProductThumbnail,
		// Token: 0x040012DC RID: 4828
		[MailMergeCode(eMailMergeCodeGroup.InventoryProducts, "productimage", "Inventory product picture", typeof(string), "")]
		InventoryProducts_ProductImage,
		// Token: 0x040012DD RID: 4829
		[MailMergeCode(eMailMergeCodeGroup.InventoryProductLoans, "loanid", "Inventory loan id", typeof(int), "")]
		InventoryProductLoan_LoanId,
		// Token: 0x040012DE RID: 4830
		[MailMergeCode(eMailMergeCodeGroup.InventoryProductLoans, "loaneddate", "Inventory loaned date", typeof(DateTime), "")]
		InventoryProductLoan_LoanedDate,
		// Token: 0x040012DF RID: 4831
		[MailMergeCode(eMailMergeCodeGroup.InventoryProductLoans, "loanduedate", "Inventory loan due date", typeof(DateTime), "")]
		InventoryProductLoan_DueDate,
		// Token: 0x040012E0 RID: 4832
		[MailMergeCode(eMailMergeCodeGroup.InventoryProductLoans, "loannotes", "Inventory loan notes", typeof(string), "")]
		InventoryProductLoan_LoanNotes,
		// Token: 0x040012E1 RID: 4833
		[MailMergeCode(eMailMergeCodeGroup.InventoryProductLoans, "wholoanedpersonid", "Inventory loan by staff personid", typeof(int), "")]
		InventoryProductLoan_WhoLoanedPersonId,
		// Token: 0x040012E2 RID: 4834
		[MailMergeCode(eMailMergeCodeGroup.InventoryProductLoans, "wholoanedfirstname", "Inventory loan by staff firstname", typeof(string), "")]
		InventoryProductLoan_WhoLoanedFirstname,
		// Token: 0x040012E3 RID: 4835
		[MailMergeCode(eMailMergeCodeGroup.InventoryProductLoans, "wholoanedlastname", "Inventory loan by staff lastname", typeof(string), "")]
		InventoryProductLoan_WhoLoanedLastname,
		// Token: 0x040012E4 RID: 4836
		[MailMergeCode(eMailMergeCodeGroup.InventoryProductLoans, "loanlocation", "Inventory loan location", typeof(string), "")]
		InventoryProductLoan_Location,
		// Token: 0x040012E5 RID: 4837
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatMediaContent, "alternateformatmediacontentid", "Alternate format media content id", typeof(Guid), "")]
		AlternateFormatMediaContent_MediaContentId,
		// Token: 0x040012E6 RID: 4838
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatMediaContent, "alternateformatmediacontenttitle", "Alternate format media content title", typeof(string), "")]
		AlternateFormatMediaContent_MediaContentTitle,
		// Token: 0x040012E7 RID: 4839
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatMediaContent, "alternateformatmediacontentisbn", "Alternate format media content ISBN", typeof(string), "")]
		AlternateFormatMediaContent_MediaContentISBN,
		// Token: 0x040012E8 RID: 4840
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatRequests, "alternateformatrequestid", "Alternate format request id", typeof(int), "")]
		AlternateFormatRequest_AlternateFormatRequestId,
		// Token: 0x040012E9 RID: 4841
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatRequests, "alternateformatrequeststatus", "Alternate format request status", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatRequestStatus,
		// Token: 0x040012EA RID: 4842
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatRequests, "alternateformatrequestavailablestarttime", "Alternate format request available start time", typeof(DateTime), "")]
		AlternateFormatRequest_AlternateFormatRequestAvailableStartTime,
		// Token: 0x040012EB RID: 4843
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatRequests, "alternateformatrequestavailableendtime", "Alternate format request available end time", typeof(DateTime), "")]
		AlternateFormatRequest_AlternateFormatRequestAvailableEndTime,
		// Token: 0x040012EC RID: 4844
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatRequests, "alternateformatrequestmediacontenttitle", "Alternate format request media content title", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatRequestMediaContentTitle,
		// Token: 0x040012ED RID: 4845
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatRequests, "alternateformatrequestmediacontentformat", "Alternate format request media content format", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatRequestMediaContentFormat,
		// Token: 0x040012EE RID: 4846
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatRequests, "alternateformatrequestmediacontentisbn", "Alternate format request media content ISBN", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatRequestMediaContentISBN,
		// Token: 0x040012EF RID: 4847
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatRequests, "alternateformatrequestmediacontentauthors", "Alternate format request media content authors", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatRequestMediaContentAuthors,
		// Token: 0x040012F0 RID: 4848
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatRequests, "alternateformatrequestmediacontentedition", "Alternate format request media content edition", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatRequestMediaContentEdition,
		// Token: 0x040012F1 RID: 4849
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatRequests, "alternateformatrequestcampus", "Alternate format request campus", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatRequestMediaContentCampus,
		// Token: 0x040012F2 RID: 4850
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatRequests, "alternateformatrequestcreateddatetime", "Alternate format request created datetime", typeof(DateTime), "")]
		AlternateFormatRequest_AlternateFormatRequestCreatedDatetime,
		// Token: 0x040012F3 RID: 4851
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatRequests, "alternateformatrequestcompleteddatetime", "Alternate format request completed datetime", typeof(DateTime), "")]
		AlternateFormatRequest_AlternateFormatRequestCompletedDatetime,
		// Token: 0x040012F4 RID: 4852
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatpublisherid", "Alternate format publisher id", typeof(int), "")]
		AlternateFormatRequest_AlternateFormatPublisherId,
		// Token: 0x040012F5 RID: 4853
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatpublishername", "Alternate format publisher name", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatPublisherName,
		// Token: 0x040012F6 RID: 4854
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatpublisheremail", "Alternate format publisher email", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatPublisherEmail,
		// Token: 0x040012F7 RID: 4855
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatpublisherphone", "Alternate format publisher phone", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatPublisherPhone,
		// Token: 0x040012F8 RID: 4856
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatpublisherfax", "Alternate format publisher fax", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatPublisherFax,
		// Token: 0x040012F9 RID: 4857
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatpublisherwebsite", "Alternate format publisher website", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatPublisherWebsite,
		// Token: 0x040012FA RID: 4858
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatpublisheraddress", "Alternate format publisher address", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatPublisherAddress,
		// Token: 0x040012FB RID: 4859
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatvendorid", "Alternate format vendor id", typeof(int), "")]
		AlternateFormatRequest_AlternateFormatVendorId,
		// Token: 0x040012FC RID: 4860
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatvendorname", "Alternate format vendor name", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatVendorName,
		// Token: 0x040012FD RID: 4861
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatvendoremail", "Alternate format vendor email", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatVendorEmail,
		// Token: 0x040012FE RID: 4862
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatvendorphone", "Alternate format vendor phone", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatVendorPhone,
		// Token: 0x040012FF RID: 4863
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatvendorfax", "Alternate format vendor fax", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatVendorFax,
		// Token: 0x04001300 RID: 4864
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatvendorwebsite", "Alternate format vendor website", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatVendorWebsite,
		// Token: 0x04001301 RID: 4865
		[MailMergeCode(eMailMergeCodeGroup.AlternateFormatPublishers, "alternateformatvendoraddress", "Alternate format vendor address", typeof(string), "")]
		AlternateFormatRequest_AlternateFormatVendorAddress,
		// Token: 0x04001302 RID: 4866
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceprovidername", "Service Provider name", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderName,
		// Token: 0x04001303 RID: 4867
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceproviderfirstname", "Service Provider first name", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderFirstName,
		// Token: 0x04001304 RID: 4868
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceproviderlastname", "Service Provider last name", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderLastName,
		// Token: 0x04001305 RID: 4869
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceprovidermiddlename", "Service Provider middle name", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderMiddleName,
		// Token: 0x04001306 RID: 4870
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceproviderstudentnumber", "Service Provider student number", typeof(string), "", new string[]
		{
			"serviceproviderstudent_no"
		})]
		SERVICEPROVIDERS_ServiceProviderStudentNumber,
		// Token: 0x04001307 RID: 4871
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceproviderusername", "Service Provider username", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderUsername,
		// Token: 0x04001308 RID: 4872
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceprovideremail", "Service Provider email", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderEmail,
		// Token: 0x04001309 RID: 4873
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceproviderphone1", "Service Provider phone 1", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderPhone1,
		// Token: 0x0400130A RID: 4874
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceproviderphone2", "Service Provider phone 2", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderPhone2,
		// Token: 0x0400130B RID: 4875
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceprovideremail2", "Service Provider email 2", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderEmail2,
		// Token: 0x0400130C RID: 4876
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceproviderspecialization", "Service Provider specialization", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderSpecialization,
		// Token: 0x0400130D RID: 4877
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceprovideraddress", "Service Provider address", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderAddress,
		// Token: 0x0400130E RID: 4878
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceprovideraddress2", "Service Provider address 2", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderAddress2,
		// Token: 0x0400130F RID: 4879
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceprovidernotes", "Service Provider provider notes", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderNotes,
		// Token: 0x04001310 RID: 4880
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceprovideradditionalnote", "Service Provider additional note", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderAdditionalNote,
		// Token: 0x04001311 RID: 4881
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceproviderregistrationcomplete", "Service Provider registration is complete", typeof(bool), "Yes")]
		SERVICEPROVIDERS_ServiceProviderRegistrationComplete,
		// Token: 0x04001312 RID: 4882
		[MailMergeCode(eMailMergeCodeGroup.ServiceProviders, "serviceprovideractiveaddress", "Service Provider active address", typeof(string), "")]
		SERVICEPROVIDERS_ServiceProviderActiveAddress
	}
}
