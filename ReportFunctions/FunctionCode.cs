using System;

namespace ReportFunctions
{
	// Token: 0x02000002 RID: 2
	public enum FunctionCode
	{
		// Token: 0x04000002 RID: 2
		Unknown = -1,
		// Token: 0x04000003 RID: 3
		Sql_Query,
		// Token: 0x04000004 RID: 4
		Sql_Query_Dynamic_Data,
		// Token: 0x04000005 RID: 5
		NOP,
		// Token: 0x04000006 RID: 6
		Breakdown_Numbers_Dynamic_Data,
		// Token: 0x04000007 RID: 7
		Decrypt_Data,
		// Token: 0x04000008 RID: 8
		Sort,
		// Token: 0x04000009 RID: 9
		Run_Another_Report,
		// Token: 0x0400000A RID: 10
		Remove_Items_With_Specific_Value,
		// Token: 0x0400000B RID: 11
		Reorder_Columns,
		// Token: 0x0400000C RID: 12
		Map_Cells_to_Columns,
		// Token: 0x0400000D RID: 13
		Merge_Rows,
		// Token: 0x0400000E RID: 14
		Remove_Columns,
		// Token: 0x0400000F RID: 15
		Rename_Columns,
		// Token: 0x04000010 RID: 16
		Combine_Columns,
		// Token: 0x04000011 RID: 17
		Map_Column_Names_to_Specific_Values,
		// Token: 0x04000012 RID: 18
		Move_Data_to_Other_Columns_for_Specific_Rows,
		// Token: 0x04000013 RID: 19
		Concatenate_Column_Cell_Data_Text,
		// Token: 0x04000014 RID: 20
		Search_and_Replace_Case_Sensitive,
		// Token: 0x04000015 RID: 21
		Remove_Extra_Spaces_From_Comma_Separated_List,
		// Token: 0x04000016 RID: 22
		Mark_Rows_as_Special_That_Have_Differing_Values_for_Unique_Row_Groups,
		// Token: 0x04000017 RID: 23
		Remove_Duplicate_Rows,
		// Token: 0x04000018 RID: 24
		Extract_and_Return_Rows_With_Temp_or_Invalid_Student_Numbers,
		// Token: 0x04000019 RID: 25
		Remove_Rows_With_Temp_or_Invalid_Student_Numbers,
		// Token: 0x0400001A RID: 26
		Breakdown_Numbers,
		// Token: 0x0400001B RID: 27
		Keep_Only_Duplicate_Rows,
		// Token: 0x0400001C RID: 28
		Force_Specific_Columns_in_a_Specific_Order,
		// Token: 0x0400001D RID: 29
		Split_Col_Data_into_Multiple_Columns,
		// Token: 0x0400001E RID: 30
		Execute_and_Merge_in_Another_Report,
		// Token: 0x0400001F RID: 31
		Stamp_Current_Table,
		// Token: 0x04000020 RID: 32
		Run_Another_Report_and_Concatenate_the_Results_to_the_Current_Table,
		// Token: 0x04000021 RID: 33
		Add_New_Columns,
		// Token: 0x04000022 RID: 34
		Change_Column_DataTypes,
		// Token: 0x04000023 RID: 35
		Add_New_Columns_Dynamic,
		// Token: 0x04000024 RID: 36
		Sql_Query_Dynamic_Data_Keep_Rows_Without_Data_Info,
		// Token: 0x04000025 RID: 37
		Run_Another_Report_and_Concatenate_UNIQUE_Results_to_the_Current_Table,
		// Token: 0x04000026 RID: 38
		Create_New_Boolean_Columns_from_Unique_Values_in_a_Column,
		// Token: 0x04000027 RID: 39
		Multiple_Rows_One_for_each_Value_in_a_Delimiter_Separated_Column_Cell,
		// Token: 0x04000028 RID: 40
		Merge_Rows_Exclude_Duplicate_Items_in_Comma_Separated_Lists,
		// Token: 0x04000029 RID: 41
		Add_Time_Duration_Column,
		// Token: 0x0400002A RID: 42
		Add_Column_with_Count_of_Delimitered_Items_in_Another_Column,
		// Token: 0x0400002B RID: 43
		Set_Variables,
		// Token: 0x0400002C RID: 44
		Run_Another_Report_Without_Collecting_Parameters_From_the_User,
		// Token: 0x0400002D RID: 45
		Set_All_Blank_Cells_to_NULL,
		// Token: 0x0400002E RID: 46
		Merge_Accommodations_for_Students_With_2_Rows_of_Accommodations,
		// Token: 0x0400002F RID: 47
		Sql_Query_Dynamic_Data_2_Per_Student,
		// Token: 0x04000030 RID: 48
		Sql_Query_Dynamic_Data_2_Per_Appointment,
		// Token: 0x04000031 RID: 49
		Encrypt_Data,
		// Token: 0x04000032 RID: 50
		Import_User_Data,
		// Token: 0x04000033 RID: 51
		Sql_Query_from_External_Table,
		// Token: 0x04000034 RID: 52
		Insert_Rows_From_Current_Table_Into_a_Database_Table,
		// Token: 0x04000035 RID: 53
		Backup_ClockWork_Database,
		// Token: 0x04000036 RID: 54
		Export_Data,
		// Token: 0x04000037 RID: 55
		Import_User_Data_TEST,
		// Token: 0x04000038 RID: 56
		Merge_Rows_by_Removing_Duplicate_Rows,
		// Token: 0x04000039 RID: 57
		Explode_Rows_for_Per_Screen_List_Data,
		// Token: 0x0400003A RID: 58
		Drop_Day_From_Dates_Only_Keep_Month_and_Year,
		// Token: 0x0400003B RID: 59
		Extract_Unique_Students_With_Row_Having_the_Min_Max_Value_In_a_Specific_Column,
		// Token: 0x0400003C RID: 60
		Decrypt_and_Fix_Appointment_Memos,
		// Token: 0x0400003D RID: 61
		Cross_Reference_With_Per_Student_Data,
		// Token: 0x0400003E RID: 62
		Execute_Function_Against_Memory_Table,
		// Token: 0x0400003F RID: 63
		Pull_in_Data_Using_Sql,
		// Token: 0x04000040 RID: 64
		Sort_Attendees_Into_Staff_Facilitator_and_Client_Groups_With_Counts,
		// Token: 0x04000041 RID: 65
		Import_Students_Courses,
		// Token: 0x04000042 RID: 66
		Split_Strings,
		// Token: 0x04000043 RID: 67
		Find_Personids,
		// Token: 0x04000044 RID: 68
		Extract_Unique_Rows,
		// Token: 0x04000045 RID: 69
		Divide_and_Conquer,
		// Token: 0x04000046 RID: 70
		Remove_Duplicate_Items_From_Comma_Separated_List,
		// Token: 0x04000047 RID: 71
		Add_Boolean_Count_Across_Columns,
		// Token: 0x04000048 RID: 72
		Load_All_Active_Students_With_Specific_Data = 70,
		// Token: 0x04000049 RID: 73
		Breakdown_Checkbox_Counts = 80,
		// Token: 0x0400004A RID: 74
		Cross_Reference_With_Accommodations,
		// Token: 0x0400004B RID: 75
		Import_from_formatted_text_file,
		// Token: 0x0400004C RID: 76
		Delete_file,
		// Token: 0x0400004D RID: 77
		Only_keep_first_row_for_each_group,
		// Token: 0x0400004E RID: 78
		Execute_command_line,
		// Token: 0x0400004F RID: 79
		Name_a_table,
		// Token: 0x04000050 RID: 80
		Add_students_to_master_student_table_in_memory,
		// Token: 0x04000051 RID: 81
		Make_a_table_the_current_table,
		// Token: 0x04000052 RID: 82
		Write_Table_to_OleDb_Database,
		// Token: 0x04000053 RID: 83
		Batch_Email_with_Mail_Merge,
		// Token: 0x04000054 RID: 84
		Write_Data_CUSTOM_DATA,
		// Token: 0x04000055 RID: 85
		Write_Data_CUSTOM_COURSES,
		// Token: 0x04000056 RID: 86
		Data_Sync_Update_All_Students,
		// Token: 0x04000057 RID: 87
		Consume_Web_Service,
		// Token: 0x04000058 RID: 88
		Import_CSV_File,
		// Token: 0x04000059 RID: 89
		Split2,
		// Token: 0x0400005A RID: 90
		Date_Add,
		// Token: 0x0400005B RID: 91
		If_then_else,
		// Token: 0x0400005C RID: 92
		Copy_Columns,
		// Token: 0x0400005D RID: 93
		CustomFunctions_Fanshawe,
		// Token: 0x0400005E RID: 94
		Remove_Rows_By_Comparison_Operator,
		// Token: 0x0400005F RID: 95
		Right,
		// Token: 0x04000060 RID: 96
		Left,
		// Token: 0x04000061 RID: 97
		Search_and_Replace_Case_INsensitive,
		// Token: 0x04000062 RID: 98
		Course_Calculate_Start_End_Dates,
		// Token: 0x04000063 RID: 99
		Only_Keep_Rows_Where_a_Column_has_a_matching_value,
		// Token: 0x04000064 RID: 100
		Date_fix,
		// Token: 0x04000065 RID: 101
		Rows_to_columns_DynamicScreenFormat_for_per_appointment_data,
		// Token: 0x04000066 RID: 102
		Run_Custom_Function,
		// Token: 0x04000067 RID: 103
		CustomFunctions_Fanshawe_Changed,
		// Token: 0x04000068 RID: 104
		Remove_Non_ClockWork_Students,
		// Token: 0x04000069 RID: 105
		Cross_reference_per_app_data2,
		// Token: 0x0400006A RID: 106
		Remove_Rows,
		// Token: 0x0400006B RID: 107
		Convert_Timetable_to_ClockWork_Timetable,
		// Token: 0x0400006C RID: 108
		Freeze_Table,
		// Token: 0x0400006D RID: 109
		Merge_Primary_and_Secondary_Columns,
		// Token: 0x0400006E RID: 110
		Execute_Script,
		// Token: 0x0400006F RID: 111
		Combine_Boolean_Columns,
		// Token: 0x04000070 RID: 112
		Import_CSV_File_Directly_to_ClockWork_Table,
		// Token: 0x04000071 RID: 113
		Hide_Columns,
		// Token: 0x04000072 RID: 114
		Import_Tab_Delimitered_Directly_to_ClockWork_Table,
		// Token: 0x04000073 RID: 115
		Parameters_Collection,
		// Token: 0x04000074 RID: 116
		Filter_Rows,
		// Token: 0x04000075 RID: 117
		Decode_Dynamic_Data,
		// Token: 0x04000076 RID: 118
		Export_to_xml,
		// Token: 0x04000077 RID: 119
		Decrypt_Dynamic_Data,
		// Token: 0x04000078 RID: 120
		Import_MS_Access_Table,
		// Token: 0x04000079 RID: 121
		Export_to_csv,
		// Token: 0x0400007A RID: 122
		Cross_Reference_With_Accommodations2,
		// Token: 0x0400007B RID: 123
		Decrypt_and_fix_dynamic_data,
		// Token: 0x0400007C RID: 124
		Batch_Email_with_Mail_Merge_3 = 135,
		// Token: 0x0400007D RID: 125
		Execute_Script_2,
		// Token: 0x0400007E RID: 126
		Data_Sync_Courses_2,
		// Token: 0x0400007F RID: 127
		Data_Sync_Service_Provider_Data,
		// Token: 0x04000080 RID: 128
		Data_Sync_Service_Provider_Courses,
		// Token: 0x04000081 RID: 129
		Execute_Basic_Oracle_Query = 150
	}
}
