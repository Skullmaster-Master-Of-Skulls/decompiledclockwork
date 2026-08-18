using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Net.Http
{
	// Token: 0x02000005 RID: 5
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class SR
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal SR()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceMan == null)
				{
					ResourceManager resourceManager = new ResourceManager("System.Net.Http.SR", typeof(SR).Assembly);
					SR.resourceMan = resourceManager;
				}
				return SR.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002091 File Offset: 0x00000291
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002098 File Offset: 0x00000298
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return SR.resourceCulture;
			}
			set
			{
				SR.resourceCulture = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020A0 File Offset: 0x000002A0
		internal static string net_http_argument_empty_string
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_argument_empty_string", SR.resourceCulture);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020B6 File Offset: 0x000002B6
		internal static string net_http_client_absolute_baseaddress_required
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_client_absolute_baseaddress_required", SR.resourceCulture);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020CC File Offset: 0x000002CC
		internal static string net_http_client_content_headers
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_client_content_headers", SR.resourceCulture);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E2 File Offset: 0x000002E2
		internal static string net_http_client_execution_error
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_client_execution_error", SR.resourceCulture);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020F8 File Offset: 0x000002F8
		internal static string net_http_client_http_baseaddress_required
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_client_http_baseaddress_required", SR.resourceCulture);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000210E File Offset: 0x0000030E
		internal static string net_http_client_invalid_requesturi
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_client_invalid_requesturi", SR.resourceCulture);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002124 File Offset: 0x00000324
		internal static string net_http_client_request_already_sent
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_client_request_already_sent", SR.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000213A File Offset: 0x0000033A
		internal static string net_http_client_request_headers
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_client_request_headers", SR.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002150 File Offset: 0x00000350
		internal static string net_http_client_response_headers
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_client_response_headers", SR.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002166 File Offset: 0x00000366
		internal static string net_http_client_send_canceled
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_client_send_canceled", SR.resourceCulture);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000217C File Offset: 0x0000037C
		internal static string net_http_client_send_completed
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_client_send_completed", SR.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002192 File Offset: 0x00000392
		internal static string net_http_client_send_error
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_client_send_error", SR.resourceCulture);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021A8 File Offset: 0x000003A8
		internal static string net_http_content_buffersize_exceeded
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_content_buffersize_exceeded", SR.resourceCulture);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021BE File Offset: 0x000003BE
		internal static string net_http_content_buffersize_limit
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_content_buffersize_limit", SR.resourceCulture);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021D4 File Offset: 0x000003D4
		internal static string net_http_content_encoding_set
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_content_encoding_set", SR.resourceCulture);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021EA File Offset: 0x000003EA
		internal static string net_http_content_field_too_long
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_content_field_too_long", SR.resourceCulture);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002200 File Offset: 0x00000400
		internal static string net_http_content_invalid_charset
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_content_invalid_charset", SR.resourceCulture);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002216 File Offset: 0x00000416
		internal static string net_http_content_no_task_returned
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_content_no_task_returned", SR.resourceCulture);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000222C File Offset: 0x0000042C
		internal static string net_http_content_readonly_stream
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_content_readonly_stream", SR.resourceCulture);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002242 File Offset: 0x00000442
		internal static string net_http_content_stream_already_read
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_content_stream_already_read", SR.resourceCulture);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002258 File Offset: 0x00000458
		internal static string net_http_content_stream_copy_error
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_content_stream_copy_error", SR.resourceCulture);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000226E File Offset: 0x0000046E
		internal static string net_http_copyto_array_too_small
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_copyto_array_too_small", SR.resourceCulture);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002284 File Offset: 0x00000484
		internal static string net_http_handler_nocontentlength
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_handler_nocontentlength", SR.resourceCulture);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000229A File Offset: 0x0000049A
		internal static string net_http_handler_norequest
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_handler_norequest", SR.resourceCulture);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022B0 File Offset: 0x000004B0
		internal static string net_http_handler_noresponse
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_handler_noresponse", SR.resourceCulture);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022C6 File Offset: 0x000004C6
		internal static string net_http_handler_not_assigned
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_handler_not_assigned", SR.resourceCulture);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022DC File Offset: 0x000004DC
		internal static string net_http_headers_cant_add_any_to_collection
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_headers_cant_add_any_to_collection", SR.resourceCulture);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000022F2 File Offset: 0x000004F2
		internal static string net_http_headers_invalid_etag_name
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_headers_invalid_etag_name", SR.resourceCulture);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002308 File Offset: 0x00000508
		internal static string net_http_headers_invalid_from_header
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_headers_invalid_from_header", SR.resourceCulture);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000231E File Offset: 0x0000051E
		internal static string net_http_headers_invalid_header_name
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_headers_invalid_header_name", SR.resourceCulture);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002334 File Offset: 0x00000534
		internal static string net_http_headers_invalid_host_header
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_headers_invalid_host_header", SR.resourceCulture);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000234A File Offset: 0x0000054A
		internal static string net_http_headers_invalid_range
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_headers_invalid_range", SR.resourceCulture);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002360 File Offset: 0x00000560
		internal static string net_http_headers_invalid_value
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_headers_invalid_value", SR.resourceCulture);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002376 File Offset: 0x00000576
		internal static string net_http_headers_no_newlines
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_headers_no_newlines", SR.resourceCulture);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000238C File Offset: 0x0000058C
		internal static string net_http_headers_not_allowed_header_name
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_headers_not_allowed_header_name", SR.resourceCulture);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023A2 File Offset: 0x000005A2
		internal static string net_http_headers_not_found
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_headers_not_found", SR.resourceCulture);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023B8 File Offset: 0x000005B8
		internal static string net_http_headers_single_value_header
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_headers_single_value_header", SR.resourceCulture);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000023CE File Offset: 0x000005CE
		internal static string net_http_httpmethod_format_error
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_httpmethod_format_error", SR.resourceCulture);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000023E4 File Offset: 0x000005E4
		internal static string net_http_invalid_enable_first
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_invalid_enable_first", SR.resourceCulture);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000023FA File Offset: 0x000005FA
		internal static string net_http_log_content_no_task_returned_copytoasync
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_log_content_no_task_returned_copytoasync", SR.resourceCulture);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002410 File Offset: 0x00000610
		internal static string net_http_log_content_null
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_log_content_null", SR.resourceCulture);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002426 File Offset: 0x00000626
		internal static string net_http_log_content_offload_async
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_log_content_offload_async", SR.resourceCulture);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000243C File Offset: 0x0000063C
		internal static string net_http_log_headers_invalid_quality
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_log_headers_invalid_quality", SR.resourceCulture);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002452 File Offset: 0x00000652
		internal static string net_http_log_headers_invalid_value
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_log_headers_invalid_value", SR.resourceCulture);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002468 File Offset: 0x00000668
		internal static string net_http_log_headers_no_newlines
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_log_headers_no_newlines", SR.resourceCulture);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000247E File Offset: 0x0000067E
		internal static string net_http_log_headers_wrong_email_format
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_log_headers_wrong_email_format", SR.resourceCulture);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002494 File Offset: 0x00000694
		internal static string net_http_message_not_success_statuscode
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_message_not_success_statuscode", SR.resourceCulture);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000024AA File Offset: 0x000006AA
		internal static string net_http_operation_started
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_operation_started", SR.resourceCulture);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000024C0 File Offset: 0x000006C0
		internal static string net_http_parser_invalid_base64_string
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_parser_invalid_base64_string", SR.resourceCulture);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000024D6 File Offset: 0x000006D6
		internal static string net_http_parser_invalid_date_format
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_parser_invalid_date_format", SR.resourceCulture);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000024EC File Offset: 0x000006EC
		internal static string net_http_read_error
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_read_error", SR.resourceCulture);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002502 File Offset: 0x00000702
		internal static string net_http_reasonphrase_format_error
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_reasonphrase_format_error", SR.resourceCulture);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002518 File Offset: 0x00000718
		internal static string net_http_securityprotocolnotsupported
		{
			get
			{
				return SR.ResourceManager.GetString("net_http_securityprotocolnotsupported", SR.resourceCulture);
			}
		}

		// Token: 0x04000050 RID: 80
		private static ResourceManager resourceMan;

		// Token: 0x04000051 RID: 81
		private static CultureInfo resourceCulture;
	}
}
