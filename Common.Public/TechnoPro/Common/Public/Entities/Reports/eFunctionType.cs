using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x0200021E RID: 542
	[Serializable]
	public enum eFunctionType
	{
		// Token: 0x04000E6F RID: 3695
		Unknown = -1,
		// Token: 0x04000E70 RID: 3696
		[ReportFunctionType("Sql Query", "SqlQuery", "parameters = sql code", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorSql, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		Sql_Query,
		// Token: 0x04000E71 RID: 3697
		[ReportFunctionType("Legacy: Sql query dynamic data", "SqlQueryDynamicData", "parameters = sql code", true, OnlyAvailableOnServer = true)]
		Sql_Query_Dynamic_Data,
		// Token: 0x04000E72 RID: 3698
		[ReportFunctionType("Do nothing (no-operation NOP)", "Nop", "Doesn't do anything - you can use this to temporarily disable a function.  The parameters will not be erased.", false)]
		NOP,
		// Token: 0x04000E73 RID: 3699
		[ReportFunctionType("Legacy: Breakdown numbers dynamic data", "BreakdownNumbersDynamicData", "parameters = list of column names to breakdown", true, OnlyAvailableOnServer = true)]
		Breakdown_Numbers_Dynamic_Data,
		// Token: 0x04000E74 RID: 3700
		[ReportFunctionType("Decrypt data", "DecryptData", "parameters = [optional [optional encryption type]`[optional encryption key]`]list of column names to decrypt.  You can optionally enclose a settingcode number in #< and ># to pull in the password from the settingsgroups table (encrypted and converted to base64 using main clockwork db pwd).  Leave the column list blank to decrypt all columns.", false, OnlyAvailableOnServer = true)]
		Decrypt_Data,
		// Token: 0x04000E75 RID: 3701
		[ReportFunctionType("Legacy: Sort", "Sort", "parameters is list of comma separated column names to sort by (in order)", false, OnlyAvailableOnServer = true)]
		Sort,
		// Token: 0x04000E76 RID: 3702
		[ReportFunctionType("Legacy: Run another report", "RunAnotherReport", "parameters = report number", false)]
		Run_Another_Report,
		// Token: 0x04000E77 RID: 3703
		[ReportFunctionType("Legacy: Remove items with specific value", "LegacyOperation", "parameters = column name, item value.  Put a ! at the beginning of the column name to inverse", false, OnlyAvailableOnServer = true)]
		Remove_Items_With_Specific_Value,
		// Token: 0x04000E78 RID: 3704
		[ReportFunctionType("Legacy: Re-order columns", "LegacyOperation", "parameters = comma separated list of column names (or indices) in desired orders", false, OnlyAvailableOnServer = true)]
		Reorder_Columns,
		// Token: 0x04000E79 RID: 3705
		[ReportFunctionType("Legacy: Map cells to columns", "LegacyOperation", "parameters = name of column with values that will become new columns, name of column with values that will be put in the corresponding new columns, optional comma separated list of unique column names for automatic merging of rows so each student will get one row of data.  the ` + screen num is optional and will force the system to lookup the datatypes for each column based on the control names (dyanmic screens).  Only controls in the screen number specified will be checked.  Unknown datatypes will revert to string values.", false, OnlyAvailableOnServer = true)]
		Map_Cells_to_Columns,
		// Token: 0x04000E7A RID: 3706
		[ReportFunctionType("Legacy: Merge rows", "LegacyOperation", "parameters = list of comma separated unique column names", false, OnlyAvailableOnServer = true)]
		Merge_Rows,
		// Token: 0x04000E7B RID: 3707
		[ReportFunctionType("Legacy: Remove columns", "LegacyOperation", "parameters = list of comma sepaarted column names to remove", false, OnlyAvailableOnServer = true)]
		Remove_Columns,
		// Token: 0x04000E7C RID: 3708
		[ReportFunctionType("Rename column(s)", "RenameColumns", "parameters = list of comma separted name/value pairs with oldname=newname: ex. firstname=newfirstname,lastname=newlastname.", false, OnlyAvailableOnServer = true)]
		Rename_Columns,
		// Token: 0x04000E7D RID: 3709
		[ReportFunctionType("Legacy: Combine columns", "LegacyOperation", "parameters = ` separated list of comma separated col names; ex. col1,col2`col3,col4,col5 will merge col1 with col2 AND merge col3,col4 and col5", false, OnlyAvailableOnServer = true)]
		Combine_Columns,
		// Token: 0x04000E7E RID: 3710
		[ReportFunctionType("Legacy: Map column names to specific values", "LegacyOperation", "parameters = A ` separated list of columnname=colval1[,colval2,colval3...].  Any one of colval1,colval2... matching will be replaced with the column name. For a column, if you want to replace the text of a specific value with the column name. Can handle multiple column mappings, but doesn't work for comma separated cell values - i.e. it is expecting a single value and will do a non-case-sensitive exact string match.", false, OnlyAvailableOnServer = true)]
		Map_Column_Names_to_Specific_Values,
		// Token: 0x04000E7F RID: 3711
		[ReportFunctionType("Legacy: Move data to other columns for specific rows", "LegacyOperation", "parameters = comma separated list of ColName=ColValue name/value pairs - row will match if any of those match up ` comma separated list of column names - new columns will be created by appending '2' onto these columnnames, then values for matching rows will be copied to the new columns and blanked out from the original columns.", false, OnlyAvailableOnServer = true)]
		Move_Data_to_Other_Columns_for_Specific_Rows,
		// Token: 0x04000E80 RID: 3712
		[ReportFunctionType("Legacy: Concatenate column cell data text", "LegacyOperation", "parameters = ` separated list of string concatenations.  Each string concatenation is a destination column name + '=' + comma separated list of column names and string values - column names should be enclosed in square brackets. Destination column will be created (as string) if it doesn't exist.  Use <comma> for comma string.", false, OnlyAvailableOnServer = true)]
		Concatenate_Column_Cell_Data_Text,
		// Token: 0x04000E81 RID: 3713
		[ReportFunctionType("Legacy: Search and replace case-sensitive", "LegacyOperation", "parameters = ` separated list (3 items): colname`searchstring`replacestring", false, OnlyAvailableOnServer = true)]
		Search_and_Replace_Case_Sensitive,
		// Token: 0x04000E82 RID: 3714
		[ReportFunctionType("Legacy: Remove extra spaces from comma separated list", "LegacyOperation", "parameters = A comma separated list of column names.  Removes extra spaces from a column containing comma-separated values", false, OnlyAvailableOnServer = true)]
		Remove_Extra_Spaces_From_Comma_Separated_List,
		// Token: 0x04000E83 RID: 3715
		[ReportFunctionType("Legacy: Mark rows as special that have differing values for unique row groups", "LegacyOperation", "parameters = A ` separated list (3 items): 1. newSpecialColumnName - the name to give a new boolean column that will be created - true will mean that there is something different in the allshouldbethesame columns.  2. uniqueRowColNames - a comma separated list of column names that determine which rows are in a group (i.e. equivalent).  3. allShouldBeTheSameColNames - a comma separated list of column names that should have the exact same values for equivalent rows.", false, OnlyAvailableOnServer = true)]
		Mark_Rows_as_Special_That_Have_Differing_Values_for_Unique_Row_Groups,
		// Token: 0x04000E84 RID: 3716
		[ReportFunctionType("Legacy: Remove duplicate rows", "LegacyOperation", "parameters = Comma separated list of col names that determine equivalent rows, optional ` + 1|0 -> 1 means don't remove the first duplicate row encountered, 0 means remove all duplicate rows including the first one.  Default is 1.", false, OnlyAvailableOnServer = true)]
		Remove_Duplicate_Rows,
		// Token: 0x04000E85 RID: 3717
		[ReportFunctionType("Legacy: Extract and return rows with temp or invalid student numbers", "LegacyOperation", "parameters = name of column holding student numbers [, number of characters in a valid student number].  If optional component is not specified no checking will be done, otherwise all student numbers with lengths not matching the number specified will also be returned.", false, OnlyAvailableOnServer = true)]
		Extract_and_Return_Rows_With_Temp_or_Invalid_Student_Numbers,
		// Token: 0x04000E86 RID: 3718
		[ReportFunctionType("Legacy: Remove rows with temp or invalid student numbers", "LegacyOperation", "parameters = name of column holding student numbers [, number of characters in a valid student number].  If optional component is not specified no checking will be done, otherwise all student numbers with lengths not matching the number specified will also be removed.", false, OnlyAvailableOnServer = true)]
		Remove_Rows_With_Temp_or_Invalid_Student_Numbers,
		// Token: 0x04000E87 RID: 3719
		[ReportFunctionType("Legacy: Breakdown numbers", "BreakdownNumbers", "Gives a total row count for each unique row; parameters = comma separated list of column names for columns that identify unique rows.  Optionally add NEWLINE + comma separated list of items that should show up in the breakdown even if they had a count of zero (normally wouldn't show up).  A column name can be {1,2,65,9}, where the numbers (control ids) will point to lookup controlcaptions.", false, OnlyAvailableOnServer = true)]
		Breakdown_Numbers,
		// Token: 0x04000E88 RID: 3720
		[ReportFunctionType("Legacy: Keep only duplicate rows", "LegacyOperation", "parameters = Comma separated list of col names that determine equivalent rows.", false, OnlyAvailableOnServer = true)]
		Keep_Only_Duplicate_Rows,
		// Token: 0x04000E89 RID: 3721
		[ReportFunctionType("Legacy: Force specific columns in specific order", "LegacyOperation", "Supply a list of column names and data types and this function will re-order the results columns to match - if there are extra columns in the results they will be dropped, and if there are missing columns empty columns will be added.  Parameters = comma separated list of name`value pairs (colum name`data type) - use ERRORONMISSINGCOLUMNS`ERRORONMISSINGCOLUMNS to generate an error if there is a missing column (must be first item in list).  The following data types are valid: string, bool, boolean, datetime, int32", false, OnlyAvailableOnServer = true)]
		Force_Specific_Columns_in_a_Specific_Order,
		// Token: 0x04000E8A RID: 3722
		[ReportFunctionType("Legacy: Split col data into multiple columns", "LegacyOperation", "takes data in a column and splits it between the existing column and a new column you specify, based on cell values (you say which values will be put into the new column).  This function supports multiple by separating with a carriage return.  FORMAT: existing column name`new column name`val1,val2,val3*,*val4,val*5[<ENTER>] between multiple entries", false, OnlyAvailableOnServer = true)]
		Split_Col_Data_into_Multiple_Columns,
		// Token: 0x04000E8B RID: 3723
		[ReportFunctionType("Legacy: Execute and merge in another report", "RunAnotherReportAndMergeInToCurrentTable", "Executes another report and merges the results with the current table - takes a set of column names that match up between the 2 tables - any rows that don't exist in the current table will not exist in the final results (i.e. pulls in from the other report).  Parameters = REPORT NUMBER[~NAMEVALUEPAIRPARAMS1;NAMEVALUEPAIRPARAMS2;...]`COMMA SEPARATED LIST OF UNIQUE COL NAMES`EXTERNAL COLUMN NAMES TO IMPORT`OPTIONAL COMMA SEPARATED NAME=VALUE pairs of columns to rename in the external table ex. Oldcolname1=newcolname1,oldcolname2=newcolname2", false)]
		Execute_and_Merge_in_Another_Report,
		// Token: 0x04000E8C RID: 3724
		[ReportFunctionType("Legacy: Stamp current table", "LegacyOperation", "Adds a new column with the same value for every row in the current table.  NewColName`DataType(bool,str,int)`value", false, OnlyAvailableOnServer = true)]
		Stamp_Current_Table,
		// Token: 0x04000E8D RID: 3725
		[ReportFunctionType("Legacy: Run another report and concatenate results to current table", "RunAnotherReportAndConcatenateResultsToCurrentTable", "Executes another report and concatenates the results to the current table.  Column data in the new table will be placed in corresponding columns in the current table (ie. The col name matches), if the column doesn't exist then new columns will be added to the current table as needed.  REPORTNUM`OPTIONAL_OLDCOLNAME=NEWCOLNAME_NAMEVALUEPAIRS`OPTIONAL_STAMPTABLE_COLNAME,DATATYPE(BOOL,STRING,INT),VALUE", false)]
		Run_Another_Report_and_Concatenate_the_Results_to_the_Current_Table,
		// Token: 0x04000E8E RID: 3726
		[ReportFunctionType("Add column(s)", "AddColumns", "Adds one or more new columns to a table - only if they don't already exist.  NEWCOLNAME,DATATYPE,INITVAL`...", false, OnlyAvailableOnServer = true)]
		Add_New_Columns,
		// Token: 0x04000E8F RID: 3727
		[ReportFunctionType("Legacy: Change column data types", "LegacyOperation", "Changes one or more columns into a specific datatype.  Possible conversions include string-bool,string-int,string-datetime,int-bool,bool-int,bool-string,int-string.  If the column already is the data type specified, the data will not be touched for that column  COLNAME,DATATYPE`COLNAME2,DATATYPE2...", false, OnlyAvailableOnServer = true)]
		Change_Column_DataTypes,
		// Token: 0x04000E90 RID: 3728
		[ReportFunctionType("Legacy: Add new columns dynamic", "LegacyOperation", "Adds one or more new columns to a table - only if they don't already exist - col names are generated from a sql command.  SQL_1stCol_is_colname_Optional2ndCol_is_datatype", false, OnlyAvailableOnServer = true)]
		Add_New_Columns_Dynamic,
		// Token: 0x04000E91 RID: 3729
		[ReportFunctionType("Legacy: Sql query dynamic data keep rows without data info", "SqlQueryDynamicData", "Same as SQL Query Dynamic Data, but this one will retain any rows with student information and no dynamic data info (controlid)", false, OnlyAvailableOnServer = true)]
		Sql_Query_Dynamic_Data_Keep_Rows_Without_Data_Info,
		// Token: 0x04000E92 RID: 3730
		[ReportFunctionType("Legacy: Run another report and concatenate UNIQUE results to current table", "RunAnotherReportAndConcatenateUniqueResultsToCurrentTable", "Same as 'Run another report and concatenate results...', but this one only adds in rows that don't already exist in the current table (based on a set of matching columns you specify).  REPORTNUMBER`MATCHINGCOLS`COLSTOIMPORT`OPTIONAL_LISTOF_OLDCOLNAME=NEWCOLNAME_NAMEVALUEPAIRS`OPTIONALSTAMP", false)]
		Run_Another_Report_and_Concatenate_UNIQUE_Results_to_the_Current_Table,
		// Token: 0x04000E93 RID: 3731
		[ReportFunctionType("Map single column values to checkbox columns", "CreateNewBooleanColumns", "Unique column values are not case sensitive.  Parameter is just the column name of a column with string values.  Each unique string value will be created as a new boolean column, and checked for each row it's listed on.", false, OnlyAvailableOnServer = true)]
		Create_New_Boolean_Columns_from_Unique_Values_in_a_Column,
		// Token: 0x04000E94 RID: 3732
		[ReportFunctionType("Legacy: Multiple rows one for each value in a delimiter separated column cell", "LegacyOperation", "Looks in a specific column and splits the value for a row using the supplied delimiter - then creates a new row for each value in the list, with copies of all other columns.  PARAMETERS: colname`delimiter(use <cr> for new line, or <chr(x)>)", false, OnlyAvailableOnServer = true)]
		Multiple_Rows_One_for_each_Value_in_a_Delimiter_Separated_Column_Cell,
		// Token: 0x04000E95 RID: 3733
		[ReportFunctionType("Legacy: Merge rows exclude duplicate items in comma separated lists", "LegacyOperation", "list of comma separated unique column names", false, OnlyAvailableOnServer = true)]
		Merge_Rows_Exclude_Duplicate_Items_in_Comma_Separated_Lists,
		// Token: 0x04000E96 RID: 3734
		[ReportFunctionType("Legacy: Add time duration column", "LegacyOperation", "Adds hours column (ex. 1.5 ). Assumes the dates are the same (i.e. less than 24 hours duration). parameters=startdatetime column name, enddatetime column name", false, OnlyAvailableOnServer = true)]
		Add_Time_Duration_Column,
		// Token: 0x04000E97 RID: 3735
		[ReportFunctionType("Legacy: Add column with count of delimitered items in another column", "LegacyOperation", "new column name to create, existing column with delimitered items, optional delimiter", false, OnlyAvailableOnServer = true)]
		Add_Column_with_Count_of_Delimitered_Items_in_Another_Column,
		// Token: 0x04000E98 RID: 3736
		[ReportFunctionType("Legacy: Set variables", "SetVariables", "Explicitly set the value of one or more variables.  Parameters=a ` separated list of name/value pairs.  The name is in the form varname.datatype - allowed data types are int, double, bool, string, date", false, OnlyAvailableOnServer = true)]
		Set_Variables,
		// Token: 0x04000E99 RID: 3737
		[ReportFunctionType("Legacy: Run another report without collecting parameters from the user", "RunAnotherReportWithStaticParameters", "Same as run another report, but suppresses the parameters input box that would normally be displayed to the user.  parameters = report #`list of name/value pairs where name=varname.datatype", false)]
		Run_Another_Report_Without_Collecting_Parameters_From_the_User,
		// Token: 0x04000E9A RID: 3738
		[ReportFunctionType("Legacy: Set all blank cells to null", "LegacyOperation", "Colname", false, OnlyAvailableOnServer = true)]
		Set_All_Blank_Cells_to_NULL,
		// Token: 0x04000E9B RID: 3739
		[ReportFunctionType("Legacy: Merge accommodations for students with 2 rows of accommodations", "LegacyOperation", "Merges 2 different sets of accommodations for the same student (boolean fields will use 'or', date fields will use the max date, string fields [if 'time' is in the col name then it will extract the first number and use the max, otherwise it will contcatenate the strings]).  Parameters = cols that identify unique students`cols to ignore (optional)", false, OnlyAvailableOnServer = true)]
		Merge_Accommodations_for_Students_With_2_Rows_of_Accommodations,
		// Token: 0x04000E9C RID: 3740
		[ReportFunctionType("Legacy: Sql query dynamic data 2 per student", "SqlQueryDynamicData", "parameters = sql code.  Returns formatted, decrypted & sorted data", false, OnlyAvailableOnServer = true)]
		Sql_Query_Dynamic_Data_2_Per_Student,
		// Token: 0x04000E9D RID: 3741
		[ReportFunctionType("Legacy: Sql query dynamic data 2 per appointment", "SqlQueryDynamicData", "parameters = sql code.  Returns formatted, decrypted & sorted data", false, OnlyAvailableOnServer = true)]
		Sql_Query_Dynamic_Data_2_Per_Appointment,
		// Token: 0x04000E9E RID: 3742
		[ReportFunctionType("Legacy: Encrypt data", "EncryptData", "Comma separated list of column names to encrypt", false)]
		Encrypt_Data,
		// Token: 0x04000E9F RID: 3743
		[ReportFunctionType("Data Sync (Data)", "ImportUserData", "<newline> delimitered list of columname=controlid~params name value pairs.  Columnname is the name of the column in the external system, controlid is the control id of the matching control in ClockWork.", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorDataSyncData, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		Import_User_Data,
		// Token: 0x04000EA0 RID: 3744
		[ReportFunctionType("Sql query from external table", "SqlQueryFromExternalTable", "(type=sqlserver,oledb)`connection string`sql code", false, OnlyAvailableOnServer = true)]
		Sql_Query_from_External_Table,
		// Token: 0x04000EA1 RID: 3745
		[ReportFunctionType("Legacy: Insert rows from current table into a database table", "LegacyOperation", "(type=sqlserver,oledb)`connection string`Table name.  Note that the table should already exists (truncate it first in a sql statement if you need it cleared out) and the columns should match.", false, OnlyAvailableOnServer = true)]
		Insert_Rows_From_Current_Table_Into_a_Database_Table,
		// Token: 0x04000EA2 RID: 3746
		[ReportFunctionType("Legacy: Backup ClockWork database", "LegacyOperation", "filename (including path * databasename will be appended to the filename)`optional parameters.  Optional parameters are specified as name=value pairs and are newline delimetered.  Available parameters are secondary=7 days, delete=2 months and zipsecondary=yes.  Secondary tells the system to put aside incremental backups (ex. 7 days means copy the current backup file to a 2nd file whenever it is 7 days or more since it was created).  Delete means delete all files that match filename*.ext that are older than the specified days/weeks/months/years.  Zipsecondary means zip up the secondary files using 7zip.", false)]
		Backup_ClockWork_Database,
		// Token: 0x04000EA3 RID: 3747
		[ReportFunctionType("Legacy: Export data", "LegacyOperation", "filename (including path)`optional export type specific parameters", false)]
		Export_Data,
		// Token: 0x04000EA4 RID: 3748
		[ReportFunctionType("Legacy: Import user data TEST", "LegacyOperation", "Same as 'Import user data' but doesn't actually make any changes to the ClockWork database (for setup/testing purposes)", false)]
		Import_User_Data_TEST,
		// Token: 0x04000EA5 RID: 3749
		[ReportFunctionType("Legacy: Merge rows by removing duplicate rows", "LegacyOperation", "parameters = list of comma-separated column names that are the unique columns.  It doesn't matter if the list is sorted, this function will keep the first row it finds and drop all subsequent rows with a matching unique-column(s) data.  Any blank/null rows will be filled in with the first existing data found in a duplicate row.", false, OnlyAvailableOnServer = true)]
		Merge_Rows_by_Removing_Duplicate_Rows,
		// Token: 0x04000EA6 RID: 3750
		[ReportFunctionType("Legacy: Explode rows for per screen list data", "LegacyOperation", "When you run a report to return data for a per student screen, list controls will return all of their row and column data as a single string.  Use this function to expand that data back into table form.  Parameters are the name of the column that has the list data, then a newline, then either a 1 (just return the latest row added to the list) or 0 (return all rows in the list)", false, OnlyAvailableOnServer = true)]
		Explode_Rows_for_Per_Screen_List_Data,
		// Token: 0x04000EA7 RID: 3751
		[ReportFunctionType("Legacy: Drop day from dates only keep month and year", "LegacyOperation", "Useful for breaking down stats by month.  Comma separated list of column names for DATETIME columns (or string column with dates).", false, OnlyAvailableOnServer = true)]
		Drop_Day_From_Dates_Only_Keep_Month_and_Year,
		// Token: 0x04000EA8 RID: 3752
		[ReportFunctionType("Legacy: Extract unique students with row having min and max value in a specific column", "LegacyOperation", "Returns 1 row for each unique student in the list.  The row that is returned will be chosen based on the value in the column you specify - if you want minimum then it will return the row with the minimum value, the same for maximum.  Parameters are MIN or MAX, <ENTER>, the name of the column with the values. (<ENTER> delimitered)", false, OnlyAvailableOnServer = true)]
		Extract_Unique_Students_With_Row_Having_the_Min_Max_Value_In_a_Specific_Column,
		// Token: 0x04000EA9 RID: 3753
		[ReportFunctionType("Legacy: Decrypt and fix appointment memos", "LegacyOperation", "Decrypts and converts rtf to plain text for appointment memos.  Name_of_appointmentmemo_col`Name_of_isencrypted_col", false, OnlyAvailableOnServer = true)]
		Decrypt_and_Fix_Appointment_Memos,
		// Token: 0x04000EAA RID: 3754
		[ReportFunctionType("Cross reference with per student data", "CrossReferencePerStudentData", "no parameters required.  Expects report parameters: @perstudentscreenname, @custom_screen10", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorDynamicControlChooser, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true, FunctionEditorWinFormsArgs = "{\"DynamicFormTypesAllowed\": [0]}")]
		Cross_Reference_With_Per_Student_Data,
		// Token: 0x04000EAB RID: 3755
		[ReportFunctionType("Legacy: Execute function against memory table", "ExecuteFunctionAgainstMemoryTable", "functioncode, parameters", false)]
		Execute_Function_Against_Memory_Table,
		// Token: 0x04000EAC RID: 3756
		[ReportFunctionType("Legacy: Pull in data using sql", "PullInDataUsingSql", "sql string (use @x for variables, where x is the column name)", false, OnlyAvailableOnServer = true)]
		Pull_in_Data_Using_Sql,
		// Token: 0x04000EAD RID: 3757
		[ReportFunctionType("Legacy: Sort attendees into staff facilitator and client groups with counts", "LegacyOperation", "no parameters - expects 'firstname', 'lastname', 'student_no', 'groupid', 'appointmentid' columns. 'misccodem column is optional", false, OnlyAvailableOnServer = true)]
		Sort_Attendees_Into_Staff_Facilitator_and_Client_Groups_With_Counts,
		// Token: 0x04000EAE RID: 3758
		[ReportFunctionType("Legacy: Import students courses", "LegacyOperation", "no parameters - expects 'student_no','term','subject','course','section' to be in table", true, OnlyAvailableOnServer = true)]
		Import_Students_Courses,
		// Token: 0x04000EAF RID: 3759
		[ReportFunctionType("Legacy: Split strings", "LegacyOperation", "Splits strings in a column into multiple columns, Does NOT remove original column when done.  Parameters=column name`newcolname1`ind1`len1[,newcolname2`ind2`len2...]", false, OnlyAvailableOnServer = true)]
		Split_Strings,
		// Token: 0x04000EB0 RID: 3760
		[ReportFunctionType("Legacy: Find personids", "LegacyOperation", "Uses student_no to match up personids.  parameter is optional student_no col name  (defaults to student_no)", false, OnlyAvailableOnServer = true)]
		Find_Personids,
		// Token: 0x04000EB1 RID: 3761
		[ReportFunctionType("Extract unique rows", "ExtractUniqueRows", "Only keeps unique rows - parameter is a comma separated list of column names that identify a row.  Column values for those rows are concatenated to form the unique row value.", false, OnlyAvailableOnServer = true)]
		Extract_Unique_Rows,
		// Token: 0x04000EB2 RID: 3762
		[ReportFunctionType("Legacy: Divide and conquer", "LegacyOperation", "column name(s) to divide by, then newline, then list of function codes + newline + function parameters (separated by newline.newline)", false, OnlyAvailableOnServer = true)]
		Divide_and_Conquer,
		// Token: 0x04000EB3 RID: 3763
		[ReportFunctionType("Legacy: Remove_Duplicate_Items_From_Comma_Separated_List", "LegacyOperation", "", false, OnlyAvailableOnServer = true)]
		Remove_Duplicate_Items_From_Comma_Separated_List,
		// Token: 0x04000EB4 RID: 3764
		[ReportFunctionType("Legacy: Add_Boolean_Count_Across_Columns", "LegacyOperation", "Comma separated list of column names with boolean values, or empty string to auto select all boolean columns", false, OnlyAvailableOnServer = true)]
		Add_Boolean_Count_Across_Columns,
		// Token: 0x04000EB5 RID: 3765
		[ReportFunctionType("Legacy: Load_All_Active_Students_With_Specific_Data", "LegacyOperation", "newline delimitered.  First line is schoolyearstartdate`schoolyearenddate (or blank if these are already part of the variables), then each line gets a controlid.", false, OnlyAvailableOnServer = true)]
		Load_All_Active_Students_With_Specific_Data = 70,
		// Token: 0x04000EB6 RID: 3766
		[ReportFunctionType("Breakdown_Checkbox_Counts", "BreakdownCheckboxCounts", "Comma separated list of column names with boolean values, or empty string to auto select all boolean columns.  Optionally use ~ and then a list of column names that identify unique rows (default will be 'student_no' if not set).  Put column names in square brackets if you need to escape commas, eg: [alphabet, soup],[color]", false, OnlyAvailableOnServer = true)]
		Breakdown_Checkbox_Counts = 80,
		// Token: 0x04000EB7 RID: 3767
		[ReportFunctionType("Legacy: Cross_Reference_With_Accommodations", "LegacyOperation", "blank or A means all accommodations, then use R=showonreport,O=showother,P=show Prof,E=show Exam.  Mixing together is ok.  Codes on a second line (newline separated) mean exclude codes.  Other codes include:  G=Group,T=Extra time,O=Other,N=Enlarged,S=ScribeReader,C=Computer,X=Alone", false, OnlyAvailableOnServer = true, IsHidden = true)]
		Cross_Reference_With_Accommodations,
		// Token: 0x04000EB8 RID: 3768
		[ReportFunctionType("Legacy: Import_from_formatted_text_file", "LegacyOperation", "filename,NEWLINE,colname.startind.length,colname.startind.length,...", false)]
		Import_from_formatted_text_file,
		// Token: 0x04000EB9 RID: 3769
		[ReportFunctionType("Legacy: Delete_file", "LegacyOperation", "filename", false)]
		Delete_file,
		// Token: 0x04000EBA RID: 3770
		[ReportFunctionType("Legacy: Only_keep_first_row_for_each_group", "LegacyOperation", "list of column names that identify unique row groups (comma separated)", false, OnlyAvailableOnServer = true)]
		Only_keep_first_row_for_each_group,
		// Token: 0x04000EBB RID: 3771
		[ReportFunctionType("Legacy: Execute_command_line", "LegacyOperation", "Executes a command line.  First line is the filename including path, optional second line is the arguments", false)]
		Execute_command_line,
		// Token: 0x04000EBC RID: 3772
		[ReportFunctionType("Legacy: Name_a_table", "NameATable", "Provide a name for the current table. Optionally add a comma to the end of the table name, followed by a comma separated list of codes.  Available codes are: removeallothers, copy", false)]
		Name_a_table,
		// Token: 0x04000EBD RID: 3773
		[ReportFunctionType("Legacy: Add_students_to_master_student_table_in_memory", "AddStudentsToMasterStudentTableInMemory", "newline delimited.  Student number column, master student table name (will be created if it doesn't exist yet).", false)]
		Add_students_to_master_student_table_in_memory,
		// Token: 0x04000EBE RID: 3774
		[ReportFunctionType("Legacy: Make_a_table_the_current_table", "MakeATableTheCurrentTable", "Provide the name of the table", false)]
		Make_a_table_the_current_table,
		// Token: 0x04000EBF RID: 3775
		[ReportFunctionType("Legacy: Write_Table_to_OleDb_Database", "LegacyOperation", "The oledb connection string, then a newline, then the name of the table to write to", false)]
		Write_Table_to_OleDb_Database,
		// Token: 0x04000EC0 RID: 3776
		[ReportFunctionType("Legacy: Batch_Email_with_Mail_Merge", "LegacyOperation", "The text from the TPEmailer template.  The codes will be the column names of the current table, an email will be sent out for each row.  The results will show the status of email attempt.", false, IsHidden = true, OnlyAvailableOnServer = true)]
		Batch_Email_with_Mail_Merge,
		// Token: 0x04000EC1 RID: 3777
		[ReportFunctionType("Legacy: Write_Data_CUSTOM_DATA", "LegacyOperation", "Clears the CUSTOM_DATA table in ClockWork, then writes all rows in the current table there.  The data is encrypted using the key outlined in the ClockWork settings.", true, OnlyAvailableOnServer = true)]
		Write_Data_CUSTOM_DATA,
		// Token: 0x04000EC2 RID: 3778
		[ReportFunctionType("Legacy: Write_Data_CUSTOM_COURSES", "LegacyOperation", "Clears the CUSTOM_COURSES table in ClockWork, then writes all rows in the current table there.  The data is encrypted using the key outlined in the ClockWork settings.", true, OnlyAvailableOnServer = true)]
		Write_Data_CUSTOM_COURSES,
		// Token: 0x04000EC3 RID: 3779
		[ReportFunctionType("Batch data sync", "BatchDataSync", "Updates all students in the current table (using the custom_data and custom_courses tables)", false, OnlyAvailableOnServer = true)]
		Data_Sync_Update_All_Students,
		// Token: 0x04000EC4 RID: 3780
		[ReportFunctionType("Legacy: Consume_Web_Service", "LegacyOperation", "NEWLINE separated: url, service name, method name, extrainfo(can be blank), arg 1, arg 2, etc.", false)]
		Consume_Web_Service,
		// Token: 0x04000EC5 RID: 3781
		[ReportFunctionType("Legacy: Import_CSV_File", "LegacyOperation", "Usage: [full path/filename to csv file]{<NEWLINE>1}, where 1 means column names are specified in the first row", false)]
		Import_CSV_File,
		// Token: 0x04000EC6 RID: 3782
		[ReportFunctionType("Legacy: Split2", "LegacyOperation", "Splits strings based on a character.  Usage: [colname]<NEWLINE>[split string or character]<NEWLINE>NewColName1<NEWLINE>NewColName2<NEWLINE>...", false, OnlyAvailableOnServer = true)]
		Split2,
		// Token: 0x04000EC7 RID: 3783
		[ReportFunctionType("Legacy: Date_Add", "LegacyOperation", "Adds something to a date column (can be in string format).  Usage: [colname]<NEWLINE>[datepart - d=day,m=minute,M=month,y=year]<NEWLINE>[amountotadd - can be a column name if enclosed in square brackets]", false, OnlyAvailableOnServer = true)]
		Date_Add,
		// Token: 0x04000EC8 RID: 3784
		[ReportFunctionType("Legacy: If_then_else", "LegacyOperation", "Checks one columns value, then sets another based on a true/false comparison.  Usage: [colname]=[value]<NEWLINE>[colname_ifmatch]=[value_ifmatch]<NEWLINE>[colname_else]=[value_else]", false, OnlyAvailableOnServer = true)]
		If_then_else,
		// Token: 0x04000EC9 RID: 3785
		[ReportFunctionType("Legacy: Copy_Columns", "LegacyOperation", "Copies a column, including data, to a new column or existing column.  Usage: [colname],[colnametocopyto]<NEWLINE>[colname],[colnametocopyto]...", false, OnlyAvailableOnServer = true)]
		Copy_Columns,
		// Token: 0x04000ECA RID: 3786
		[ReportFunctionType("Legacy: CustomFunctions_Fanshawe", "LegacyOperation", "returns 2 tables, 'courses' and 'studentdata'. Noparamaters required.", true)]
		CustomFunctions_Fanshawe,
		// Token: 0x04000ECB RID: 3787
		[ReportFunctionType("Legacy: Remove_Rows_By_Comparison_Operator", "LegacyOperation", "Removes any rows matching the pattern. Use forcedatatype in square brackets beside colname to change an existing string to datetime for example.  Works on int, double, datetime, string, bit.  Usage: [colname[forcedatatype]]=[val] OR [colname]>[val] OR [colname]<[val] OR [colname]>=[val] OR [colname]<=[val] OR [colname]!=[val]", false, OnlyAvailableOnServer = true)]
		Remove_Rows_By_Comparison_Operator,
		// Token: 0x04000ECC RID: 3788
		[ReportFunctionType("Legacy: Right", "LegacyOperation", "Copies the rightmost characters to a new column.  Usage: [colnamefind]`[colnamedest - will be created if not existing]`x    where x is the number of characters", false, OnlyAvailableOnServer = true)]
		Right,
		// Token: 0x04000ECD RID: 3789
		[ReportFunctionType("Legacy: Left", "LegacyOperation", "Copies the leftmost characters to a new column.  Usage: [colnamefind]`[colnamedest - will be created if not existing]`x   where x is the number of characters on the left to copy", false, OnlyAvailableOnServer = true)]
		Left,
		// Token: 0x04000ECE RID: 3790
		[ReportFunctionType("Legacy: Search_and_Replace_Case_INsensitive", "LegacyOperation", "Will do a search&replace on a column, ignoring case.  Usage: [colname - optionally append != on the end]`[search string]`[replace string]<NEWLINE>[colname]`[searchstring]`[replacestring]<NEWLINE>...", false, OnlyAvailableOnServer = true)]
		Search_and_Replace_Case_INsensitive,
		// Token: 0x04000ECF RID: 3791
		[ReportFunctionType("Legacy: Course_Calculate_Start_End_Dates", "LegacyOperation", "Calculates what the course start/end dates should be based on a ruleset.  NEWLINE separated list of rules: term:FW:startdate:8<NEWLINE>[default]:startdate:8", false, OnlyAvailableOnServer = true)]
		Course_Calculate_Start_End_Dates,
		// Token: 0x04000ED0 RID: 3792
		[ReportFunctionType("Legacy: Only_Keep_Rows_Where_a_Column_has_a_matching_value", "LegacyOperation", "Usage: [colname]<NEWLINE>[matchitem1]<NEWLINE>[matchitem2]<NEWLINE>...", false, OnlyAvailableOnServer = true)]
		Only_Keep_Rows_Where_a_Column_has_a_matching_value,
		// Token: 0x04000ED1 RID: 3793
		[ReportFunctionType("Legacy: Date_fix", "LegacyOperation", "Takes a date string and converts it to a different forma.  Usage: [colnames comma separated]<NEWLINE>[date format - ex. m/d/y, y-m-d]", false, OnlyAvailableOnServer = true)]
		Date_fix,
		// Token: 0x04000ED2 RID: 3794
		[ReportFunctionType("Legacy: Rows_to_columns_DynamicScreenFormat_for_per_appointment_data", "LegacyOperation", "No parameters.  Expects current table has the following columns: personid,appointmentid,valint,valbytes,valdatetime", false, OnlyAvailableOnServer = true)]
		Rows_to_columns_DynamicScreenFormat_for_per_appointment_data,
		// Token: 0x04000ED3 RID: 3795
		[ReportFunctionType("Legacy: Run_Custom_Function", "LegacyOperation", "Custom function code<NEWLINE>params", false)]
		Run_Custom_Function,
		// Token: 0x04000ED4 RID: 3796
		[ReportFunctionType("Legacy: CustomFunctions_Fanshawe_Changed", "LegacyOperation", "", true)]
		CustomFunctions_Fanshawe_Changed,
		// Token: 0x04000ED5 RID: 3797
		[ReportFunctionType("Legacy: Remove_Non_ClockWork_Students", "LegacyOperation", "Optional parameter is student-no column name (first line).  Also requires a table with a 'student_no' column.  Removes all student numbers that are not non-deleted students in ClockWork.", false, OnlyAvailableOnServer = true)]
		Remove_Non_ClockWork_Students,
		// Token: 0x04000ED6 RID: 3798
		[ReportFunctionType("Legacy: Cross_reference_per_app_data2", "LegacyOperation", "comma separated control ids.  Original table requires personid and appointmentid columns.", false, OnlyAvailableOnServer = true)]
		Cross_reference_per_app_data2,
		// Token: 0x04000ED7 RID: 3799
		[ReportFunctionType("Legacy: Remove_Rows", "LegacyOperation", "Only keeps max or min data rows.  Usage: <uniquecolnames comma separated><NEWLINE><name of column with value to compare><NEWLINE><use 1 for max, 0 for min>", false, OnlyAvailableOnServer = true)]
		Remove_Rows,
		// Token: 0x04000ED8 RID: 3800
		[ReportFunctionType("Legacy: Convert_Timetable_to_ClockWork_Timetable", "LegacyOperation", "Used for data sync - converts different timetable info into ClockWork timetable info. convertFromType(default)<NEWLINE>student#colName<NEWLINE>subjectcolname<NEWLINE>coursecolname<NEWLINE>parameters.  Parameters for default convertfromtype are: dayOfWeekColName,startTimeColname,endTimeColName.", true, OnlyAvailableOnServer = true)]
		Convert_Timetable_to_ClockWork_Timetable,
		// Token: 0x04000ED9 RID: 3801
		[ReportFunctionType("Legacy: Freeze_Table", "LegacyOperation", "Parameters = the name of the table.  Optionally comma separate a list of table names and the current table will be copied multiple times.", false)]
		Freeze_Table,
		// Token: 0x04000EDA RID: 3802
		[ReportFunctionType("Legacy: Merge_Primary_and_Secondary_Columns", "LegacyOperation", "Merges the primary data with the secondary data so that a specific data field is marked as true whether it was in the primary or secondary.  Parameters = the primary column name.", false, OnlyAvailableOnServer = true)]
		Merge_Primary_and_Secondary_Columns,
		// Token: 0x04000EDB RID: 3803
		[ReportFunctionType("Legacy: Execute_Script", "ExecuteCSharp", "Execute c# code on the current table.  Parameters = c# code.  The provided code will be run as a function on each row in the current table; passed variables are dv (current DataView) and dr (current DataRow).", true)]
		Execute_Script,
		// Token: 0x04000EDC RID: 3804
		[ReportFunctionType("Legacy: Combine_Boolean_Columns", "LegacyOperation", "Retains original columns, will create a new column with merged value.  Parameters: colname1,colname2,colname3…<NEWLINE>newcolname<NEWLINE>[AND][OR]", false, OnlyAvailableOnServer = true)]
		Combine_Boolean_Columns,
		// Token: 0x04000EDD RID: 3805
		[ReportFunctionType("Legacy: Import_CSV_File_Directly_to_ClockWork_Table", "ImportCsvDirectlyToClockWorkTable", "Goes through the CSV one row at a time and moves it into a ClockWork table.  This function doesn't need to store the entire dataset in memory and is useful for working with large .csv files.  Parameters: filename<NEWLINE>Are column names included in the first row? (1 or 0)<NEWLINE>comma separated column-index listing of columns that should be encrypted.<NEWLINE>Name of table in ClockWork (custom_courses2, custom_data2).  Note that you will have to create the table (custom_courses2 or custom_data2) appropriately before being able to use this funciton.", true, OnlyAvailableOnServer = true)]
		Import_CSV_File_Directly_to_ClockWork_Table,
		// Token: 0x04000EDE RID: 3806
		[ReportFunctionType("Legacy: Hide_Columns", "HideColumns", "Hides the columns from the view.  Parameters: comma separated list of column names to hide.  Note: column names specified that don't exist will be ignored.", false, OnlyAvailableOnServer = true)]
		Hide_Columns,
		// Token: 0x04000EDF RID: 3807
		[ReportFunctionType("Legacy: Import_Tab_Delimitered_Directly_to_ClockWork_Table", "ImportTabDelimiteredDirectlyToClockWorkTable", "Goes through the tab delimitered file one row at a time and moves it into a ClockWork table.  This function doesn't need to store the entire dataset in memory is useful for working with large .csv files.  Parameters: filename<NEWLINE>Are column names included in the first row? (1 or 0)<NEWLINE>comma separated column-index listing of columns that should be encrypted.<NEWLINE>Name of table in ClockWork (custom_courses2, custom_data2)<OPTIONALNEWLINE>delimiterchar.  Note that you will have to create the table (custom_courses2 or custom_data2) appropriately before being able to use this function.", true, OnlyAvailableOnServer = true)]
		Import_Tab_Delimitered_Directly_to_ClockWork_Table,
		// Token: 0x04000EE0 RID: 3808
		[ReportFunctionType("Parameters Collection", "LegacyOperation", "Provides a definition for a form to collect info from the user.  This is an xml definition and can be generated using the Forms Editor in the ClockWork admin.", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorDynamicForm, Common.UI.WinForms.Reports")]
		Parameters_Collection,
		// Token: 0x04000EE1 RID: 3809
		[ReportFunctionType("Legacy: Filter_Rows", "FilterRows", "Sets a row filter on the table.  Uses the default dataview row filter format: startdate>'5/1/09' AND startdate<'5/1/10'.", false, OnlyAvailableOnServer = true)]
		Filter_Rows,
		// Token: 0x04000EE2 RID: 3810
		[ReportFunctionType("Legacy: Decode_Dynamic_Data", "DecodeDynamicData", "Takes dynamic data (controlid,controlcode,setting1,setting2,etc.) and changes it into column data.   Parameters: comma separated list of column names that identify a nunique row.", false, OnlyAvailableOnServer = true)]
		Decode_Dynamic_Data,
		// Token: 0x04000EE3 RID: 3811
		[ReportFunctionType("Legacy: Export_to_xml", "LegacyOperation", "Exports the table to an xml file.  The parameters are just the full path and file name - if the file already exists it will be overwritten.", false)]
		Export_to_xml,
		// Token: 0x04000EE4 RID: 3812
		[ReportFunctionType("Legacy: Decrypt_Dynamic_Data", "LegacyOperation", "No parameters.  Expects 'valtext','valbytes' and 'valbytesisencrypted' columns", false, OnlyAvailableOnServer = true)]
		Decrypt_Dynamic_Data,
		// Token: 0x04000EE5 RID: 3813
		[ReportFunctionType("Legacy: Import_MS_Access_Table", "LegacyOperation", "Import MS Access file.  Parameters are full path and filename of mdb`table name`sql", false, IsHidden = true)]
		Import_MS_Access_Table,
		// Token: 0x04000EE6 RID: 3814
		[ReportFunctionType("Export_to_csv", "ExportToCSV", "Exports to a csv file.  Parameters are full path and filename of csv to overwrite or create.", false)]
		Export_to_csv,
		// Token: 0x04000EE7 RID: 3815
		[ReportFunctionType("Legacy: Cross_Reference_With_Accommodations2", "LegacyOperation", "Expects personid column, optional lucourseid column.  Parameters are a comma-separated list of control ids.", false, OnlyAvailableOnServer = true)]
		Cross_Reference_With_Accommodations2,
		// Token: 0x04000EE8 RID: 3816
		[ReportFunctionType("Legacy: Decrypt_and_fix_dynamic_data", "LegacyOperation", "Expects 'valtext','valbytes' and 'valbytesisencrypted' columns.  Parameters are a comma-separated list of columns that specify unique rows (ex. Personid)", false, OnlyAvailableOnServer = true)]
		Decrypt_and_fix_dynamic_data,
		// Token: 0x04000EE9 RID: 3817
		[ReportFunctionType("Batch_Email_with_Mail_Merge_3", "BatchEmailWithMailMerge3", "Note that smtp settings must be entered in the 'System' area for 'everyone settings' in order for this to work.", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorBatchEmail3, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		Batch_Email_with_Mail_Merge_3 = 135,
		// Token: 0x04000EEA RID: 3818
		[ReportFunctionType("Execute c# script (legacy)", "ExecuteCSharp", "C# Code (formerly Execute c# same application domain no file)", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorCSharpLegacy, Common.UI.WinForms.Reports")]
		Execute_Script_2,
		// Token: 0x04000EEB RID: 3819
		[ReportFunctionType("Data sync (courses)", "DataSyncCourses2", "No parameters required", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorNoParametersRequired, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		Data_Sync_Courses_2,
		// Token: 0x04000EEC RID: 3820
		[ReportFunctionType("Legacy: Data_Sync_Service_Provider_Data", "LegacyOperation", IsHidden = true, OnlyAvailableOnServer = true)]
		Data_Sync_Service_Provider_Data,
		// Token: 0x04000EED RID: 3821
		[ReportFunctionType("Legacy: Data_Sync_Service_Provider_Courses", "LegacyOperation", IsHidden = true, OnlyAvailableOnServer = true)]
		Data_Sync_Service_Provider_Courses,
		// Token: 0x04000EEE RID: 3822
		[ReportFunctionType("Oracle Query", "OracleQuery", "Requires odp.net client files to be in bin folder on server and client/web", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorOracleQuery, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		Execute_Basic_Oracle_Query = 150,
		// Token: 0x04000EEF RID: 3823
		[ReportFunctionType("Cross reference active courses", "CrossReferenceActiveCourses", IsHidden = true, OnlyAvailableOnServer = true)]
		Data_Sync_Cross_Reference_Active_Courses,
		// Token: 0x04000EF0 RID: 3824
		[ReportFunctionType("Execute c# code", "ExecuteCSharpCode", "c# code", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorCSharp, Common.UI.WinForms.Reports")]
		Execute_CSharp,
		// Token: 0x04000EF1 RID: 3825
		[ReportFunctionType("Load online intake data", "LoadOnlineIntakeData", "", "", OnlyAvailableOnServer = true)]
		LoadOnlineIntakeData,
		// Token: 0x04000EF2 RID: 3826
		[ReportFunctionType("Pre defined step: Active students with accommodations", "ActiveStudentsWithAccommodations", "Requires @startdate and @enddate", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorNoParametersRequired, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		PreDefinedStep_ActiveStudentsWithTemplateAccommodations = 200,
		// Token: 0x04000EF3 RID: 3827
		[ReportFunctionType("Pre defined step: Active students", "ActiveStudents", "Requires @startdate and @enddate", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorNoParametersRequired, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		PreDefinedStep_ActiveStudents,
		// Token: 0x04000EF4 RID: 3828
		[ReportFunctionType("Pre defined step: Active students with courses", "ActiveStudentsWithCourses", "Requires @startdate and @enddate", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorNoParametersRequired, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		PreDefinedStep_ActiveStudentsWithCourses,
		// Token: 0x04000EF5 RID: 3829
		[ReportFunctionType("Data sync intake data", "DataSyncIntakeData", "Requires @student_no", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorNoParametersRequired, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		DataSyncIntakeData,
		// Token: 0x04000EF6 RID: 3830
		[ReportFunctionType("Import Excel", "ImportExcel", "Parameters are filename with path and worksheet name", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorImportExcel, Common.UI.WinForms.Reports")]
		ImportExcel,
		// Token: 0x04000EF7 RID: 3831
		[ReportFunctionType("Load per student data", "LoadPerStudentData", "", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorSql, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		LoadPerStudentData,
		// Token: 0x04000EF8 RID: 3832
		[ReportFunctionType("Load per appointment data", "LoadPerAppointmentData", "", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorSql, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		LoadPerAppointmentData,
		// Token: 0x04000EF9 RID: 3833
		[ReportFunctionType("Load per date data", "LoadPerDateData", "", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorSql, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		LoadPerDateData,
		// Token: 0x04000EFA RID: 3834
		[ReportFunctionType("Load accommodation data", "LoadAccommodationData", "", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorSql, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		LoadAccommodationData,
		// Token: 0x04000EFB RID: 3835
		[ReportFunctionType("Load appointments", "LoadAppointments", "Report parameters will override settings put on this function step.  Parameter names are: startdate, enddate, includecancelled, users | students | staff | pids | personids, groups | gids, type", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorLoadAppointments, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		LoadAppointments,
		// Token: 0x04000EFC RID: 3836
		[ReportFunctionType("Expand list-view or file-list data", "ExpandListViewOrFileList", "parameters = comma separated list of column names; eg: Notes,Cases.  Column names can optionally have the controlid in square brackets after the name; eg: Notes[32],Cases[45].", false, OnlyAvailableOnServer = true)]
		ExpandListViewOrFileList,
		// Token: 0x04000EFD RID: 3837
		[ReportFunctionType("Import custom_courses contents to lookup courses", "ImportCustomCourses", "No parameters required", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorNoParametersRequired, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		ImportCustomCourses,
		// Token: 0x04000EFE RID: 3838
		[ReportFunctionType("Sql Query Extended", "SqlQueryExtended", "parameters = xml: {<sqlqueryextendedparameters overridetimeout='600' sql='select * from groups' />}", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorSqlExtended, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		SqlQueryExtended,
		// Token: 0x04000EFF RID: 3839
		[ReportFunctionType("Custom data write", "CustomDataWrite", "Writes data to one of the CUSTOM_xxx tables from a file", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorFillCustomData, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		WriteCustomData,
		// Token: 0x04000F00 RID: 3840
		[ReportFunctionType("Custom data load", "CustomDataLoad", "Loads data from one of the CUSTOM_xxx tables", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorLoadCustomData, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		LoadCustomData,
		// Token: 0x04000F01 RID: 3841
		[ReportFunctionType("Data sync - Move data into ClockWork", "DataSync_MoveDataIntoClockWork", "For flat file data syncs - moves data in files into ClockWork custom tables", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorDataSyncMoveDataIntoClockWork, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true, IsHidden = true)]
		DataSync_MoveDataIntoClockWork,
		// Token: 0x04000F02 RID: 3842
		[ReportFunctionType("Data sync - Load data from ClockWork custom tables", "DataSync_LoadDataFromClockWork", "For flat file data syncs - loads data from ClockWork custom tables", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorDataSyncLoadDataFromCustomTables, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		DataSync_LoadDataFromClockWork,
		// Token: 0x04000F03 RID: 3843
		[ReportFunctionType("Data sync - Fix notetaking addresses", "DataSync_FixAddressesForNotetaking", "Creates new columns with merged address info (street,city,state,postal,country) into a single newline separated entry like a mailing label.", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorDataSyncFixAddresses, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		DataSync_FixAddressesForNotetaking,
		// Token: 0x04000F04 RID: 3844
		[ReportFunctionType("Data sync - Fix timetables", "DataSync_FixTimetable", "Fixes incoming timetable information so that each row stores a single day of week and start/end time", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorDataSyncFixTimetable, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		DataSync_FixTimetable,
		// Token: 0x04000F05 RID: 3845
		[ReportFunctionType("Data sync - add LastDataSync", "DataSync_AddLastDataSync", "Adds a column called 'LastDataSync' to current table with current date/time.  No parameters required.", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorNoParametersRequired, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		DataSync_AddLastDataSync,
		// Token: 0x04000F06 RID: 3846
		[ReportFunctionType("Data sync - execute REST Web Service", "DataSync_ExecuteRestWebService", "Loads data from REST web service", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorExecuteRestWebService, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		DataSync_ExecuteRestWebService,
		// Token: 0x04000F07 RID: 3847
		[ReportFunctionType("Data sync (lookup courses only - no registrations)", "DataSyncLookupCourses", "No parameters required", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorNoParametersRequired, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true)]
		Data_Sync_Lookup_Courses,
		// Token: 0x04000F08 RID: 3848
		[ReportFunctionType("Trim spaces from people/rooms/resources", "TrimSpacesFromNames", "Trims spaces from beginning and end of all firstname/middlename/lastname for all objects in the database", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorNoParametersRequired, Common.UI.WinForms.Reports")]
		Trim_Spaces_From_All_Names,
		// Token: 0x04000F09 RID: 3849
		[ReportFunctionType("Batch data sync old courses", "BatchDataSyncOldCourses", "Data syncs only courses from the past.  Won't drop any courses ever.", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorDataSyncBatchParameters, Common.UI.WinForms.Reports")]
		Batch_Data_Sync_Old_Courses,
		// Token: 0x04000F0A RID: 3850
		[ReportFunctionType("Data sync old courses for an individual student", "DataSyncOldCourses", "Data syncs only courses from the past.  Won't drop any courses ever.", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorNoParametersRequired, Common.UI.WinForms.Reports")]
		DataSync_OldCourses,
		// Token: 0x04000F0B RID: 3851
		[ReportFunctionType("Cross reference with accommodation data", "CrossReferenceAccommodationData", "", "TechnoPro.Common.UI.WinForms.Reports.Controls.Editor.FunctionEditors.CtrlFunctionEditorDynamicControlChooser, Common.UI.WinForms.Reports", OnlyAvailableOnServer = true, FunctionEditorWinFormsArgs = "{\"DynamicFormTypesAllowed\": [3, 4]}")]
		CrossReference_With_Accommodations
	}
}
