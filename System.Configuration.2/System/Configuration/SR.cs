using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.Configuration
{
	// Token: 0x020000A8 RID: 168
	internal sealed class SR
	{
		// Token: 0x060006A0 RID: 1696 RVA: 0x0001F470 File Offset: 0x0001D670
		internal SR()
		{
			this.resources = new ResourceManager("System.Configuration", base.GetType().Assembly);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001F494 File Offset: 0x0001D694
		private static SR GetLoader()
		{
			if (SR.loader == null)
			{
				SR value = new SR();
				Interlocked.CompareExchange<SR>(ref SR.loader, value, null);
			}
			return SR.loader;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x000088C2 File Offset: 0x00006AC2
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001F4C0 File Offset: 0x0001D6C0
		public static ResourceManager Resources
		{
			get
			{
				return SR.GetLoader().resources;
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001F4CC File Offset: 0x0001D6CC
		public static string GetString(string name, params object[] args)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			string @string = sr.resources.GetString(name, SR.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001F54C File Offset: 0x0001D74C
		public static string GetString(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetString(name, SR.Culture);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001F575 File Offset: 0x0001D775
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return SR.GetString(name);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001F580 File Offset: 0x0001D780
		public static object GetObject(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetObject(name, SR.Culture);
		}

		// Token: 0x0400037F RID: 895
		internal const string Parameter_Invalid = "Parameter_Invalid";

		// Token: 0x04000380 RID: 896
		internal const string Parameter_NullOrEmpty = "Parameter_NullOrEmpty";

		// Token: 0x04000381 RID: 897
		internal const string Property_NullOrEmpty = "Property_NullOrEmpty";

		// Token: 0x04000382 RID: 898
		internal const string Property_Invalid = "Property_Invalid";

		// Token: 0x04000383 RID: 899
		internal const string Unexpected_Error = "Unexpected_Error";

		// Token: 0x04000384 RID: 900
		internal const string Wrapped_exception_message = "Wrapped_exception_message";

		// Token: 0x04000385 RID: 901
		internal const string Config_error_loading_XML_file = "Config_error_loading_XML_file";

		// Token: 0x04000386 RID: 902
		internal const string Config_exception_creating_section_handler = "Config_exception_creating_section_handler";

		// Token: 0x04000387 RID: 903
		internal const string Config_exception_creating_section = "Config_exception_creating_section";

		// Token: 0x04000388 RID: 904
		internal const string Config_tag_name_invalid = "Config_tag_name_invalid";

		// Token: 0x04000389 RID: 905
		internal const string Argument_AddingDuplicate = "Argument_AddingDuplicate";

		// Token: 0x0400038A RID: 906
		internal const string Config_add_configurationsection_already_added = "Config_add_configurationsection_already_added";

		// Token: 0x0400038B RID: 907
		internal const string Config_add_configurationsection_already_exists = "Config_add_configurationsection_already_exists";

		// Token: 0x0400038C RID: 908
		internal const string Config_add_configurationsection_in_location_config = "Config_add_configurationsection_in_location_config";

		// Token: 0x0400038D RID: 909
		internal const string Config_add_configurationsectiongroup_already_added = "Config_add_configurationsectiongroup_already_added";

		// Token: 0x0400038E RID: 910
		internal const string Config_add_configurationsectiongroup_already_exists = "Config_add_configurationsectiongroup_already_exists";

		// Token: 0x0400038F RID: 911
		internal const string Config_add_configurationsectiongroup_in_location_config = "Config_add_configurationsectiongroup_in_location_config";

		// Token: 0x04000390 RID: 912
		internal const string Config_allow_exedefinition_error_application = "Config_allow_exedefinition_error_application";

		// Token: 0x04000391 RID: 913
		internal const string Config_allow_exedefinition_error_machine = "Config_allow_exedefinition_error_machine";

		// Token: 0x04000392 RID: 914
		internal const string Config_allow_exedefinition_error_roaminguser = "Config_allow_exedefinition_error_roaminguser";

		// Token: 0x04000393 RID: 915
		internal const string Config_appsettings_declaration_invalid = "Config_appsettings_declaration_invalid";

		// Token: 0x04000394 RID: 916
		internal const string Config_base_attribute_locked = "Config_base_attribute_locked";

		// Token: 0x04000395 RID: 917
		internal const string Config_base_collection_item_locked_cannot_clear = "Config_base_collection_item_locked_cannot_clear";

		// Token: 0x04000396 RID: 918
		internal const string Config_base_collection_item_locked = "Config_base_collection_item_locked";

		// Token: 0x04000397 RID: 919
		internal const string Config_base_cannot_add_items_above_inherited_items = "Config_base_cannot_add_items_above_inherited_items";

		// Token: 0x04000398 RID: 920
		internal const string Config_base_cannot_add_items_below_inherited_items = "Config_base_cannot_add_items_below_inherited_items";

		// Token: 0x04000399 RID: 921
		internal const string Config_base_cannot_remove_inherited_items = "Config_base_cannot_remove_inherited_items";

		// Token: 0x0400039A RID: 922
		internal const string Config_base_collection_elements_may_not_be_removed = "Config_base_collection_elements_may_not_be_removed";

		// Token: 0x0400039B RID: 923
		internal const string Config_base_collection_entry_already_exists = "Config_base_collection_entry_already_exists";

		// Token: 0x0400039C RID: 924
		internal const string Config_base_collection_entry_already_removed = "Config_base_collection_entry_already_removed";

		// Token: 0x0400039D RID: 925
		internal const string Config_base_collection_entry_not_found = "Config_base_collection_entry_not_found";

		// Token: 0x0400039E RID: 926
		internal const string Config_base_element_cannot_have_multiple_child_elements = "Config_base_element_cannot_have_multiple_child_elements";

		// Token: 0x0400039F RID: 927
		internal const string Config_base_element_default_collection_cannot_be_locked = "Config_base_element_default_collection_cannot_be_locked";

		// Token: 0x040003A0 RID: 928
		internal const string Config_base_element_locked = "Config_base_element_locked";

		// Token: 0x040003A1 RID: 929
		internal const string Config_base_expected_enum = "Config_base_expected_enum";

		// Token: 0x040003A2 RID: 930
		internal const string Config_base_expected_to_find_element = "Config_base_expected_to_find_element";

		// Token: 0x040003A3 RID: 931
		internal const string Config_base_invalid_attribute_to_lock = "Config_base_invalid_attribute_to_lock";

		// Token: 0x040003A4 RID: 932
		internal const string Config_base_invalid_attribute_to_lock_by_add = "Config_base_invalid_attribute_to_lock_by_add";

		// Token: 0x040003A5 RID: 933
		internal const string Config_base_invalid_element_key = "Config_base_invalid_element_key";

		// Token: 0x040003A6 RID: 934
		internal const string Config_base_invalid_element_to_lock = "Config_base_invalid_element_to_lock";

		// Token: 0x040003A7 RID: 935
		internal const string Config_base_invalid_element_to_lock_by_add = "Config_base_invalid_element_to_lock_by_add";

		// Token: 0x040003A8 RID: 936
		internal const string Config_base_property_is_not_a_configuration_element = "Config_base_property_is_not_a_configuration_element";

		// Token: 0x040003A9 RID: 937
		internal const string Config_base_read_only = "Config_base_read_only";

		// Token: 0x040003AA RID: 938
		internal const string Config_base_required_attribute_locked = "Config_base_required_attribute_locked";

		// Token: 0x040003AB RID: 939
		internal const string Config_base_required_attribute_lock_attempt = "Config_base_required_attribute_lock_attempt";

		// Token: 0x040003AC RID: 940
		internal const string Config_base_required_attribute_missing = "Config_base_required_attribute_missing";

		// Token: 0x040003AD RID: 941
		internal const string Config_base_section_cannot_contain_cdata = "Config_base_section_cannot_contain_cdata";

		// Token: 0x040003AE RID: 942
		internal const string Config_base_section_invalid_content = "Config_base_section_invalid_content";

		// Token: 0x040003AF RID: 943
		internal const string Config_base_unrecognized_attribute = "Config_base_unrecognized_attribute";

		// Token: 0x040003B0 RID: 944
		internal const string Config_base_unrecognized_element = "Config_base_unrecognized_element";

		// Token: 0x040003B1 RID: 945
		internal const string Config_base_unrecognized_element_name = "Config_base_unrecognized_element_name";

		// Token: 0x040003B2 RID: 946
		internal const string Config_base_value_cannot_contain = "Config_base_value_cannot_contain";

		// Token: 0x040003B3 RID: 947
		internal const string Config_cannot_edit_configurationsection_in_location_config = "Config_cannot_edit_configurationsection_in_location_config";

		// Token: 0x040003B4 RID: 948
		internal const string Config_cannot_edit_configurationsection_parentsection = "Config_cannot_edit_configurationsection_parentsection";

		// Token: 0x040003B5 RID: 949
		internal const string Config_cannot_edit_configurationsection_when_location_locked = "Config_cannot_edit_configurationsection_when_location_locked";

		// Token: 0x040003B6 RID: 950
		internal const string Config_cannot_edit_configurationsection_when_locked = "Config_cannot_edit_configurationsection_when_locked";

		// Token: 0x040003B7 RID: 951
		internal const string Config_cannot_edit_configurationsection_when_not_attached = "Config_cannot_edit_configurationsection_when_not_attached";

		// Token: 0x040003B8 RID: 952
		internal const string Config_cannot_edit_configurationsection_when_it_is_implicit = "Config_cannot_edit_configurationsection_when_it_is_implicit";

		// Token: 0x040003B9 RID: 953
		internal const string Config_cannot_edit_configurationsection_when_it_is_undeclared = "Config_cannot_edit_configurationsection_when_it_is_undeclared";

		// Token: 0x040003BA RID: 954
		internal const string Config_cannot_edit_configurationsectiongroup_in_location_config = "Config_cannot_edit_configurationsectiongroup_in_location_config";

		// Token: 0x040003BB RID: 955
		internal const string Config_cannot_edit_configurationsectiongroup_when_not_attached = "Config_cannot_edit_configurationsectiongroup_when_not_attached";

		// Token: 0x040003BC RID: 956
		internal const string Config_cannot_edit_locationattriubtes = "Config_cannot_edit_locationattriubtes";

		// Token: 0x040003BD RID: 957
		internal const string Config_cannot_open_config_source = "Config_cannot_open_config_source";

		// Token: 0x040003BE RID: 958
		internal const string Config_client_config_init_error = "Config_client_config_init_error";

		// Token: 0x040003BF RID: 959
		internal const string Config_client_config_init_security = "Config_client_config_init_security";

		// Token: 0x040003C0 RID: 960
		internal const string Config_client_config_too_many_configsections_elements = "Config_client_config_too_many_configsections_elements";

		// Token: 0x040003C1 RID: 961
		internal const string Config_configmanager_open_noexe = "Config_configmanager_open_noexe";

		// Token: 0x040003C2 RID: 962
		internal const string Config_configsection_parentnotvalid = "Config_configsection_parentnotvalid";

		// Token: 0x040003C3 RID: 963
		internal const string Config_connectionstrings_declaration_invalid = "Config_connectionstrings_declaration_invalid";

		// Token: 0x040003C4 RID: 964
		internal const string Config_data_read_count_mismatch = "Config_data_read_count_mismatch";

		// Token: 0x040003C5 RID: 965
		internal const string Config_element_no_context = "Config_element_no_context";

		// Token: 0x040003C6 RID: 966
		internal const string Config_empty_lock_attributes_except = "Config_empty_lock_attributes_except";

		// Token: 0x040003C7 RID: 967
		internal const string Config_empty_lock_attributes_except_effective = "Config_empty_lock_attributes_except_effective";

		// Token: 0x040003C8 RID: 968
		internal const string Config_empty_lock_element_except = "Config_empty_lock_element_except";

		// Token: 0x040003C9 RID: 969
		internal const string Config_exception_in_config_section_handler = "Config_exception_in_config_section_handler";

		// Token: 0x040003CA RID: 970
		internal const string Config_file_doesnt_have_root_configuration = "Config_file_doesnt_have_root_configuration";

		// Token: 0x040003CB RID: 971
		internal const string Config_file_has_changed = "Config_file_has_changed";

		// Token: 0x040003CC RID: 972
		internal const string Config_getparentconfigurationsection_first_instance = "Config_getparentconfigurationsection_first_instance";

		// Token: 0x040003CD RID: 973
		internal const string Config_inconsistent_location_attributes = "Config_inconsistent_location_attributes";

		// Token: 0x040003CE RID: 974
		internal const string Config_invalid_attributes_for_write = "Config_invalid_attributes_for_write";

		// Token: 0x040003CF RID: 975
		internal const string Config_invalid_boolean_attribute = "Config_invalid_boolean_attribute";

		// Token: 0x040003D0 RID: 976
		internal const string Config_invalid_configurationsection_constructor = "Config_invalid_configurationsection_constructor";

		// Token: 0x040003D1 RID: 977
		internal const string Config_invalid_node_type = "Config_invalid_node_type";

		// Token: 0x040003D2 RID: 978
		internal const string Config_location_location_not_allowed = "Config_location_location_not_allowed";

		// Token: 0x040003D3 RID: 979
		internal const string Config_location_path_invalid_character = "Config_location_path_invalid_character";

		// Token: 0x040003D4 RID: 980
		internal const string Config_location_path_invalid_first_character = "Config_location_path_invalid_first_character";

		// Token: 0x040003D5 RID: 981
		internal const string Config_location_path_invalid_last_character = "Config_location_path_invalid_last_character";

		// Token: 0x040003D6 RID: 982
		internal const string Config_missing_required_attribute = "Config_missing_required_attribute";

		// Token: 0x040003D7 RID: 983
		internal const string Config_more_data_than_expected = "Config_more_data_than_expected";

		// Token: 0x040003D8 RID: 984
		internal const string Config_name_value_file_section_file_invalid_root = "Config_name_value_file_section_file_invalid_root";

		// Token: 0x040003D9 RID: 985
		internal const string Config_namespace_invalid = "Config_namespace_invalid";

		// Token: 0x040003DA RID: 986
		internal const string Config_no_stream_to_write = "Config_no_stream_to_write";

		// Token: 0x040003DB RID: 987
		internal const string Config_not_allowed_to_encrypt_this_section = "Config_not_allowed_to_encrypt_this_section";

		// Token: 0x040003DC RID: 988
		internal const string Config_object_is_null = "Config_object_is_null";

		// Token: 0x040003DD RID: 989
		internal const string Config_operation_not_runtime = "Config_operation_not_runtime";

		// Token: 0x040003DE RID: 990
		internal const string Config_properties_may_not_be_derived_from_configuration_section = "Config_properties_may_not_be_derived_from_configuration_section";

		// Token: 0x040003DF RID: 991
		internal const string Config_protection_section_not_found = "Config_protection_section_not_found";

		// Token: 0x040003E0 RID: 992
		internal const string Config_provider_must_implement_type = "Config_provider_must_implement_type";

		// Token: 0x040003E1 RID: 993
		internal const string Config_root_section_group_cannot_be_edited = "Config_root_section_group_cannot_be_edited";

		// Token: 0x040003E2 RID: 994
		internal const string Config_section_allow_definition_attribute_invalid = "Config_section_allow_definition_attribute_invalid";

		// Token: 0x040003E3 RID: 995
		internal const string Config_section_allow_exe_definition_attribute_invalid = "Config_section_allow_exe_definition_attribute_invalid";

		// Token: 0x040003E4 RID: 996
		internal const string Config_section_cannot_be_used_in_location = "Config_section_cannot_be_used_in_location";

		// Token: 0x040003E5 RID: 997
		internal const string Config_section_group_missing_public_constructor = "Config_section_group_missing_public_constructor";

		// Token: 0x040003E6 RID: 998
		internal const string Config_section_locked = "Config_section_locked";

		// Token: 0x040003E7 RID: 999
		internal const string Config_sections_must_be_unique = "Config_sections_must_be_unique";

		// Token: 0x040003E8 RID: 1000
		internal const string Config_source_cannot_be_shared = "Config_source_cannot_be_shared";

		// Token: 0x040003E9 RID: 1001
		internal const string Config_source_parent_conflict = "Config_source_parent_conflict";

		// Token: 0x040003EA RID: 1002
		internal const string Config_source_file_format = "Config_source_file_format";

		// Token: 0x040003EB RID: 1003
		internal const string Config_source_invalid_format = "Config_source_invalid_format";

		// Token: 0x040003EC RID: 1004
		internal const string Config_source_invalid_chars = "Config_source_invalid_chars";

		// Token: 0x040003ED RID: 1005
		internal const string Config_source_requires_file = "Config_source_requires_file";

		// Token: 0x040003EE RID: 1006
		internal const string Config_source_syntax_error = "Config_source_syntax_error";

		// Token: 0x040003EF RID: 1007
		internal const string Config_system_already_set = "Config_system_already_set";

		// Token: 0x040003F0 RID: 1008
		internal const string Config_tag_name_already_defined = "Config_tag_name_already_defined";

		// Token: 0x040003F1 RID: 1009
		internal const string Config_tag_name_already_defined_at_this_level = "Config_tag_name_already_defined_at_this_level";

		// Token: 0x040003F2 RID: 1010
		internal const string Config_tag_name_cannot_be_location = "Config_tag_name_cannot_be_location";

		// Token: 0x040003F3 RID: 1011
		internal const string Config_tag_name_cannot_begin_with_config = "Config_tag_name_cannot_begin_with_config";

		// Token: 0x040003F4 RID: 1012
		internal const string Config_type_doesnt_inherit_from_type = "Config_type_doesnt_inherit_from_type";

		// Token: 0x040003F5 RID: 1013
		internal const string Config_unexpected_element_end = "Config_unexpected_element_end";

		// Token: 0x040003F6 RID: 1014
		internal const string Config_unexpected_element_name = "Config_unexpected_element_name";

		// Token: 0x040003F7 RID: 1015
		internal const string Config_unexpected_node_type = "Config_unexpected_node_type";

		// Token: 0x040003F8 RID: 1016
		internal const string Config_unrecognized_configuration_section = "Config_unrecognized_configuration_section";

		// Token: 0x040003F9 RID: 1017
		internal const string Config_write_failed = "Config_write_failed";

		// Token: 0x040003FA RID: 1018
		internal const string Converter_timespan_not_in_second = "Converter_timespan_not_in_second";

		// Token: 0x040003FB RID: 1019
		internal const string Converter_unsupported_value_type = "Converter_unsupported_value_type";

		// Token: 0x040003FC RID: 1020
		internal const string Decryption_failed = "Decryption_failed";

		// Token: 0x040003FD RID: 1021
		internal const string Default_value_conversion_error_from_string = "Default_value_conversion_error_from_string";

		// Token: 0x040003FE RID: 1022
		internal const string Default_value_wrong_type = "Default_value_wrong_type";

		// Token: 0x040003FF RID: 1023
		internal const string DPAPI_bad_data = "DPAPI_bad_data";

		// Token: 0x04000400 RID: 1024
		internal const string Empty_attribute = "Empty_attribute";

		// Token: 0x04000401 RID: 1025
		internal const string EncryptedNode_not_found = "EncryptedNode_not_found";

		// Token: 0x04000402 RID: 1026
		internal const string EncryptedNode_is_in_invalid_format = "EncryptedNode_is_in_invalid_format";

		// Token: 0x04000403 RID: 1027
		internal const string Encryption_failed = "Encryption_failed";

		// Token: 0x04000404 RID: 1028
		internal const string Expect_bool_value_for_DoNotShowUI = "Expect_bool_value_for_DoNotShowUI";

		// Token: 0x04000405 RID: 1029
		internal const string Expect_bool_value_for_useMachineProtection = "Expect_bool_value_for_useMachineProtection";

		// Token: 0x04000406 RID: 1030
		internal const string IndexOutOfRange = "IndexOutOfRange";

		// Token: 0x04000407 RID: 1031
		internal const string Invalid_enum_value = "Invalid_enum_value";

		// Token: 0x04000408 RID: 1032
		internal const string Key_container_doesnt_exist_or_access_denied = "Key_container_doesnt_exist_or_access_denied";

		// Token: 0x04000409 RID: 1033
		internal const string Must_add_to_config_before_protecting_it = "Must_add_to_config_before_protecting_it";

		// Token: 0x0400040A RID: 1034
		internal const string No_converter = "No_converter";

		// Token: 0x0400040B RID: 1035
		internal const string No_exception_information_available = "No_exception_information_available";

		// Token: 0x0400040C RID: 1036
		internal const string Property_name_reserved = "Property_name_reserved";

		// Token: 0x0400040D RID: 1037
		internal const string Item_name_reserved = "Item_name_reserved";

		// Token: 0x0400040E RID: 1038
		internal const string Basicmap_item_name_reserved = "Basicmap_item_name_reserved";

		// Token: 0x0400040F RID: 1039
		internal const string ProtectedConfigurationProvider_not_found = "ProtectedConfigurationProvider_not_found";

		// Token: 0x04000410 RID: 1040
		internal const string Regex_validator_error = "Regex_validator_error";

		// Token: 0x04000411 RID: 1041
		internal const string String_null_or_empty = "String_null_or_empty";

		// Token: 0x04000412 RID: 1042
		internal const string Subclass_validator_error = "Subclass_validator_error";

		// Token: 0x04000413 RID: 1043
		internal const string Top_level_conversion_error_from_string = "Top_level_conversion_error_from_string";

		// Token: 0x04000414 RID: 1044
		internal const string Top_level_conversion_error_to_string = "Top_level_conversion_error_to_string";

		// Token: 0x04000415 RID: 1045
		internal const string Top_level_validation_error = "Top_level_validation_error";

		// Token: 0x04000416 RID: 1046
		internal const string Type_cannot_be_resolved = "Type_cannot_be_resolved";

		// Token: 0x04000417 RID: 1047
		internal const string TypeNotPublic = "TypeNotPublic";

		// Token: 0x04000418 RID: 1048
		internal const string Unrecognized_initialization_value = "Unrecognized_initialization_value";

		// Token: 0x04000419 RID: 1049
		internal const string UseMachineContainer_must_be_bool = "UseMachineContainer_must_be_bool";

		// Token: 0x0400041A RID: 1050
		internal const string UseOAEP_must_be_bool = "UseOAEP_must_be_bool";

		// Token: 0x0400041B RID: 1051
		internal const string Validation_scalar_range_violation_not_different = "Validation_scalar_range_violation_not_different";

		// Token: 0x0400041C RID: 1052
		internal const string Validation_scalar_range_violation_not_equal = "Validation_scalar_range_violation_not_equal";

		// Token: 0x0400041D RID: 1053
		internal const string Validation_scalar_range_violation_not_in_range = "Validation_scalar_range_violation_not_in_range";

		// Token: 0x0400041E RID: 1054
		internal const string Validation_scalar_range_violation_not_outside_range = "Validation_scalar_range_violation_not_outside_range";

		// Token: 0x0400041F RID: 1055
		internal const string Validator_Attribute_param_not_validator = "Validator_Attribute_param_not_validator";

		// Token: 0x04000420 RID: 1056
		internal const string Validator_does_not_support_elem_type = "Validator_does_not_support_elem_type";

		// Token: 0x04000421 RID: 1057
		internal const string Validator_does_not_support_prop_type = "Validator_does_not_support_prop_type";

		// Token: 0x04000422 RID: 1058
		internal const string Validator_element_not_valid = "Validator_element_not_valid";

		// Token: 0x04000423 RID: 1059
		internal const string Validator_method_not_found = "Validator_method_not_found";

		// Token: 0x04000424 RID: 1060
		internal const string Validator_min_greater_than_max = "Validator_min_greater_than_max";

		// Token: 0x04000425 RID: 1061
		internal const string Validator_scalar_resolution_violation = "Validator_scalar_resolution_violation";

		// Token: 0x04000426 RID: 1062
		internal const string Validator_string_invalid_chars = "Validator_string_invalid_chars";

		// Token: 0x04000427 RID: 1063
		internal const string Validator_string_max_length = "Validator_string_max_length";

		// Token: 0x04000428 RID: 1064
		internal const string Validator_string_min_length = "Validator_string_min_length";

		// Token: 0x04000429 RID: 1065
		internal const string Validator_value_type_invalid = "Validator_value_type_invalid";

		// Token: 0x0400042A RID: 1066
		internal const string Validator_multiple_validator_attributes = "Validator_multiple_validator_attributes";

		// Token: 0x0400042B RID: 1067
		internal const string Validator_timespan_value_must_be_positive = "Validator_timespan_value_must_be_positive";

		// Token: 0x0400042C RID: 1068
		internal const string WrongType_of_Protected_provider = "WrongType_of_Protected_provider";

		// Token: 0x0400042D RID: 1069
		internal const string Type_from_untrusted_assembly = "Type_from_untrusted_assembly";

		// Token: 0x0400042E RID: 1070
		internal const string Config_element_locking_not_supported = "Config_element_locking_not_supported";

		// Token: 0x0400042F RID: 1071
		internal const string Config_element_null_instance = "Config_element_null_instance";

		// Token: 0x04000430 RID: 1072
		internal const string ConfigurationPermissionBadXml = "ConfigurationPermissionBadXml";

		// Token: 0x04000431 RID: 1073
		internal const string ConfigurationPermission_Denied = "ConfigurationPermission_Denied";

		// Token: 0x04000432 RID: 1074
		internal const string Section_from_untrusted_assembly = "Section_from_untrusted_assembly";

		// Token: 0x04000433 RID: 1075
		internal const string Protection_provider_syntax_error = "Protection_provider_syntax_error";

		// Token: 0x04000434 RID: 1076
		internal const string Protection_provider_invalid_format = "Protection_provider_invalid_format";

		// Token: 0x04000435 RID: 1077
		internal const string Cannot_declare_or_remove_implicit_section = "Cannot_declare_or_remove_implicit_section";

		// Token: 0x04000436 RID: 1078
		internal const string Config_reserved_attribute = "Config_reserved_attribute";

		// Token: 0x04000437 RID: 1079
		internal const string Filename_in_SaveAs_is_used_already = "Filename_in_SaveAs_is_used_already";

		// Token: 0x04000438 RID: 1080
		internal const string Provider_Already_Initialized = "Provider_Already_Initialized";

		// Token: 0x04000439 RID: 1081
		internal const string Config_provider_name_null_or_empty = "Config_provider_name_null_or_empty";

		// Token: 0x0400043A RID: 1082
		internal const string CollectionReadOnly = "CollectionReadOnly";

		// Token: 0x0400043B RID: 1083
		internal const string Config_source_not_under_config_dir = "Config_source_not_under_config_dir";

		// Token: 0x0400043C RID: 1084
		internal const string Config_source_invalid = "Config_source_invalid";

		// Token: 0x0400043D RID: 1085
		internal const string Location_invalid_inheritInChildApplications_in_machine_or_root_web_config = "Location_invalid_inheritInChildApplications_in_machine_or_root_web_config";

		// Token: 0x0400043E RID: 1086
		internal const string Cannot_change_both_AllowOverride_and_OverrideMode = "Cannot_change_both_AllowOverride_and_OverrideMode";

		// Token: 0x0400043F RID: 1087
		internal const string Config_section_override_mode_attribute_invalid = "Config_section_override_mode_attribute_invalid";

		// Token: 0x04000440 RID: 1088
		internal const string Invalid_override_mode_declaration = "Invalid_override_mode_declaration";

		// Token: 0x04000441 RID: 1089
		internal const string Config_cannot_edit_locked_configurationsection_when_mode_is_not_allow = "Config_cannot_edit_locked_configurationsection_when_mode_is_not_allow";

		// Token: 0x04000442 RID: 1090
		internal const string Machine_config_file_not_found = "Machine_config_file_not_found";

		// Token: 0x04000443 RID: 1091
		internal const string Config_builder_not_found = "Config_builder_not_found";

		// Token: 0x04000444 RID: 1092
		internal const string WrongType_of_config_builder = "WrongType_of_config_builder";

		// Token: 0x04000445 RID: 1093
		internal const string Config_builder_invalid_format = "Config_builder_invalid_format";

		// Token: 0x04000446 RID: 1094
		internal const string ConfigBuilder_processXml_error = "ConfigBuilder_processXml_error";

		// Token: 0x04000447 RID: 1095
		internal const string ConfigBuilder_processXml_error_short = "ConfigBuilder_processXml_error_short";

		// Token: 0x04000448 RID: 1096
		internal const string ConfigBuilder_init_error = "ConfigBuilder_init_error";

		// Token: 0x04000449 RID: 1097
		internal const string ConfigBuilder_processSection_error = "ConfigBuilder_processSection_error";

		// Token: 0x0400044A RID: 1098
		private static SR loader;

		// Token: 0x0400044B RID: 1099
		private ResourceManager resources;
	}
}
