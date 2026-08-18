using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Web.Configuration;
using System.Web.Handlers;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000100 RID: 256
	public sealed class TraceContext
	{
		// Token: 0x06000F4B RID: 3915 RVA: 0x0002BFFC File Offset: 0x0002A1FC
		public TraceContext(HttpContext context)
		{
			this._traceMode = TraceMode.Default;
			this._isEnabled = (DeploymentSection.RetailInternal ? TraceEnable.Disable : TraceEnable.Default);
			this._context = context;
			this._firstTime = -1L;
			this._lastTime = -1L;
			this._endDataCollected = false;
			this._traceRecords = new ArrayList();
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0002C05B File Offset: 0x0002A25B
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x0002C077 File Offset: 0x0002A277
		public TraceMode TraceMode
		{
			get
			{
				if (this._traceMode == TraceMode.Default)
				{
					return HttpRuntime.Profile.OutputMode;
				}
				return this._traceMode;
			}
			set
			{
				if (value < TraceMode.SortByTime || value > TraceMode.Default)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._traceMode = value;
				if (this.IsEnabled)
				{
					this.ApplyTraceMode();
				}
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0002C0A1 File Offset: 0x0002A2A1
		// (set) Token: 0x06000F4F RID: 3919 RVA: 0x0002C0C2 File Offset: 0x0002A2C2
		public bool IsEnabled
		{
			get
			{
				if (this._isEnabled == TraceEnable.Default)
				{
					return HttpRuntime.Profile.IsEnabled;
				}
				return this._isEnabled == TraceEnable.Enable;
			}
			set
			{
				if (DeploymentSection.RetailInternal)
				{
					return;
				}
				if (value)
				{
					this._isEnabled = TraceEnable.Enable;
					return;
				}
				this._isEnabled = TraceEnable.Disable;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0002C0DE File Offset: 0x0002A2DE
		internal bool PageOutput
		{
			get
			{
				if (this._isEnabled == TraceEnable.Default)
				{
					return HttpRuntime.Profile.PageOutput;
				}
				return this._isEnabled == TraceEnable.Enable;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (set) Token: 0x06000F51 RID: 3921 RVA: 0x0002C100 File Offset: 0x0002A300
		internal int StatusCode
		{
			set
			{
				this.VerifyStart();
				DataRow dataRow = this._requestData.Tables["Trace_Request"].Rows[0];
				dataRow["Trace_Status_Code"] = value;
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000F52 RID: 3922 RVA: 0x0002C145 File Offset: 0x0002A345
		// (remove) Token: 0x06000F53 RID: 3923 RVA: 0x0002C158 File Offset: 0x0002A358
		public event TraceContextEventHandler TraceFinished
		{
			add
			{
				this._events.AddHandler(TraceContext.EventTraceFinished, value);
			}
			remove
			{
				this._events.RemoveHandler(TraceContext.EventTraceFinished, value);
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0002C16C File Offset: 0x0002A36C
		private void ApplyTraceMode()
		{
			this.VerifyStart();
			if (this.TraceMode == TraceMode.SortByCategory)
			{
				this._requestData.Tables["Trace_Trace_Information"].DefaultView.Sort = "Trace_Category";
				return;
			}
			this._requestData.Tables["Trace_Trace_Information"].DefaultView.Sort = "Trace_From_First";
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0002C1D1 File Offset: 0x0002A3D1
		internal void CopySettingsTo(TraceContext tc)
		{
			tc._traceMode = this._traceMode;
			tc._isEnabled = this._isEnabled;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0002C1EC File Offset: 0x0002A3EC
		internal void OnTraceFinished(TraceContextEventArgs e)
		{
			TraceContextEventHandler traceContextEventHandler = (TraceContextEventHandler)this._events[TraceContext.EventTraceFinished];
			if (traceContextEventHandler != null)
			{
				traceContextEventHandler(this, e);
			}
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002C21A File Offset: 0x0002A41A
		internal static void SetWriteToDiagnosticsTrace(bool value)
		{
			TraceContext._writeToDiagnosticsTrace = value;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002C222 File Offset: 0x0002A422
		public void Write(string message)
		{
			this.Write(string.Empty, message, null, false, TraceContext._writeToDiagnosticsTrace);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002C237 File Offset: 0x0002A437
		public void Write(string category, string message)
		{
			this.Write(category, message, null, false, TraceContext._writeToDiagnosticsTrace);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0002C248 File Offset: 0x0002A448
		public void Write(string category, string message, Exception errorInfo)
		{
			this.Write(category, message, errorInfo, false, TraceContext._writeToDiagnosticsTrace);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002C259 File Offset: 0x0002A459
		internal void WriteInternal(string message, bool writeToDiagnostics)
		{
			this.Write(string.Empty, message, null, false, writeToDiagnostics);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002C26A File Offset: 0x0002A46A
		internal void WriteInternal(string category, string message, bool writeToDiagnostics)
		{
			this.Write(category, message, null, false, writeToDiagnostics);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0002C277 File Offset: 0x0002A477
		public void Warn(string message)
		{
			this.Write(string.Empty, message, null, true, TraceContext._writeToDiagnosticsTrace);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002C28C File Offset: 0x0002A48C
		public void Warn(string category, string message)
		{
			this.Write(category, message, null, true, TraceContext._writeToDiagnosticsTrace);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002C29D File Offset: 0x0002A49D
		public void Warn(string category, string message, Exception errorInfo)
		{
			this.Write(category, message, errorInfo, true, TraceContext._writeToDiagnosticsTrace);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002C2AE File Offset: 0x0002A4AE
		internal void WarnInternal(string category, string message, bool writeToDiagnostics)
		{
			this.Write(category, message, null, true, writeToDiagnostics);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002C2BC File Offset: 0x0002A4BC
		private void Write(string category, string message, Exception errorInfo, bool isWarning, bool writeToDiagnostics)
		{
			lock (this)
			{
				if (!this.IsEnabled || this._writing || this._endDataCollected)
				{
					return;
				}
				this.VerifyStart();
				if (category == null)
				{
					category = string.Empty;
				}
				if (message == null)
				{
					message = string.Empty;
				}
				long value = Counter.Value;
				DataRow dataRow = this.NewRow(this._requestData, "Trace_Trace_Information");
				dataRow["Trace_Category"] = category;
				dataRow["Trace_Message"] = message;
				dataRow["Trace_Warning"] = (isWarning ? "yes" : "no");
				if (errorInfo != null)
				{
					dataRow["ErrorInfoMessage"] = errorInfo.Message;
					dataRow["ErrorInfoStack"] = errorInfo.StackTrace;
				}
				if (this._firstTime != -1L)
				{
					dataRow["Trace_From_First"] = (double)(value - this._firstTime) / (double)Counter.Frequency;
				}
				else
				{
					this._firstTime = value;
				}
				if (this._lastTime != -1L)
				{
					dataRow["Trace_From_Last"] = (double)(value - this._lastTime) / (double)Counter.Frequency;
				}
				this._lastTime = value;
				this.AddRow(this._requestData, "Trace_Trace_Information", dataRow);
				string text = message;
				if (errorInfo != null)
				{
					string text2 = errorInfo.Message;
					if (text2 == null)
					{
						text2 = string.Empty;
					}
					string text3 = errorInfo.StackTrace;
					if (text3 == null)
					{
						text3 = string.Empty;
					}
					StringBuilder stringBuilder = new StringBuilder(message.Length + text2.Length + text3.Length);
					stringBuilder.Append(message);
					stringBuilder.Append(" -- ");
					stringBuilder.Append(text2);
					stringBuilder.Append(": ");
					stringBuilder.Append(text3);
					text = stringBuilder.ToString();
				}
				if (writeToDiagnostics)
				{
					this._writing = true;
					Trace.WriteLine(text, category);
					this._writing = false;
				}
				if (this._context != null && this._context.WorkerRequest != null)
				{
					this._context.WorkerRequest.RaiseTraceEvent(isWarning ? IntegratedTraceType.TraceWarn : IntegratedTraceType.TraceWrite, text);
				}
			}
			this._traceRecords.Add(new TraceContextRecord(category, message, isWarning, errorInfo));
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0002C508 File Offset: 0x0002A708
		internal void AddNewControl(string id, string parentId, string type, int viewStateSize, int controlStateSize)
		{
			this.VerifyStart();
			DataRow dataRow = this.NewRow(this._requestData, "Trace_Control_Tree");
			if (id == null)
			{
				string str = "__UnassignedID";
				int uniqueIdCounter = this._uniqueIdCounter;
				this._uniqueIdCounter = uniqueIdCounter + 1;
				id = str + uniqueIdCounter.ToString();
			}
			dataRow["Trace_Control_Id"] = id;
			if (parentId == null)
			{
				parentId = "__PAGE";
			}
			dataRow["Trace_Parent_Id"] = parentId;
			dataRow["Trace_Type"] = type;
			dataRow["Trace_Viewstate_Size"] = viewStateSize;
			dataRow["Trace_Controlstate_Size"] = controlStateSize;
			dataRow["Trace_Render_Size"] = 0;
			try
			{
				this.AddRow(this._requestData, "Trace_Control_Tree", dataRow);
			}
			catch (ConstraintException)
			{
				throw new HttpException(SR.GetString("Duplicate_id_used", new object[]
				{
					id,
					"Trace"
				}));
			}
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0002C600 File Offset: 0x0002A800
		internal void AddControlSize(string controlId, int renderSize)
		{
			this.VerifyStart();
			DataTable dataTable = this._requestData.Tables["Trace_Control_Tree"];
			if (controlId == null)
			{
				controlId = "__PAGE";
			}
			DataRow dataRow = dataTable.Rows.Find(controlId);
			if (dataRow != null)
			{
				dataRow["Trace_Render_Size"] = renderSize;
			}
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0002C654 File Offset: 0x0002A854
		internal void AddControlStateSize(string controlId, int viewstateSize, int controlstateSize)
		{
			this.VerifyStart();
			DataTable dataTable = this._requestData.Tables["Trace_Control_Tree"];
			if (controlId == null)
			{
				controlId = "__PAGE";
			}
			DataRow dataRow = dataTable.Rows.Find(controlId);
			if (dataRow != null)
			{
				dataRow["Trace_Viewstate_Size"] = viewstateSize;
				dataRow["Trace_Controlstate_Size"] = controlstateSize;
			}
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0002C6BC File Offset: 0x0002A8BC
		internal void Render(HtmlTextWriter output)
		{
			if (this.PageOutput && this._requestData != null)
			{
				TraceEnable isEnabled = this._isEnabled;
				this._isEnabled = TraceEnable.Disable;
				output.Write("<div id=\"__asptrace\">\r\n");
				output.Write(TraceHandler.StyleSheet);
				output.Write("<span class=\"tracecontent\">\r\n");
				Control control = TraceHandler.CreateDetailsTable(this._requestData.Tables["Trace_Request"]);
				if (control != null)
				{
					control.RenderControl(output);
				}
				control = TraceHandler.CreateTraceTable(this._requestData.Tables["Trace_Trace_Information"]);
				if (control != null)
				{
					control.RenderControl(output);
				}
				control = TraceHandler.CreateControlTable(this._requestData.Tables["Trace_Control_Tree"]);
				if (control != null)
				{
					control.RenderControl(output);
				}
				control = TraceHandler.CreateTable(this._requestData.Tables["Trace_Session_State"], true);
				if (control != null)
				{
					control.RenderControl(output);
				}
				control = TraceHandler.CreateTable(this._requestData.Tables["Trace_Application_State"], true);
				if (control != null)
				{
					control.RenderControl(output);
				}
				control = TraceHandler.CreateTable(this._requestData.Tables["Trace_Request_Cookies_Collection"], true);
				if (control != null)
				{
					control.RenderControl(output);
				}
				control = TraceHandler.CreateTable(this._requestData.Tables["Trace_Response_Cookies_Collection"], true);
				if (control != null)
				{
					control.RenderControl(output);
				}
				control = TraceHandler.CreateTable(this._requestData.Tables["Trace_Headers_Collection"], true);
				if (control != null)
				{
					control.RenderControl(output);
				}
				control = TraceHandler.CreateTable(this._requestData.Tables["Trace_Response_Headers_Collection"], true);
				if (control != null)
				{
					control.RenderControl(output);
				}
				control = TraceHandler.CreateTable(this._requestData.Tables["Trace_Form_Collection"]);
				if (control != null)
				{
					control.RenderControl(output);
				}
				control = TraceHandler.CreateTable(this._requestData.Tables["Trace_Querystring_Collection"]);
				if (control != null)
				{
					control.RenderControl(output);
				}
				control = TraceHandler.CreateTable(this._requestData.Tables["Trace_Server_Variables"], true);
				if (control != null)
				{
					control.RenderControl(output);
				}
				output.Write("<hr width=100% size=1 color=silver>\r\n\r\n");
				output.Write(string.Concat(new string[]
				{
					SR.GetString("Error_Formatter_CLR_Build"),
					VersionInfo.ClrVersion,
					SR.GetString("Error_Formatter_ASPNET_Build"),
					VersionInfo.EngineVersion,
					"\r\n\r\n"
				}));
				output.Write("</font>\r\n\r\n");
				output.Write("</span>\r\n</div>\r\n");
				this._isEnabled = isEnabled;
			}
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0002C93C File Offset: 0x0002AB3C
		internal DataSet GetData()
		{
			return this._requestData;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0002C944 File Offset: 0x0002AB44
		internal void VerifyStart()
		{
			if (TraceContext._masterRequest == null)
			{
				lock (this)
				{
					if (TraceContext._masterRequest == null)
					{
						this.InitMaster();
					}
				}
			}
			if (this._requestData == null)
			{
				this.InitRequest();
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002C99C File Offset: 0x0002AB9C
		internal void StopTracing()
		{
			this._endDataCollected = true;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0002C9A8 File Offset: 0x0002ABA8
		internal void EndRequest()
		{
			this.VerifyStart();
			if (this._endDataCollected)
			{
				return;
			}
			DataRow dataRow = this._requestData.Tables["Trace_Request"].Rows[0];
			dataRow["Trace_Status_Code"] = this._context.Response.StatusCode;
			dataRow["Trace_Response_Encoding"] = this._context.Response.ContentEncoding.EncodingName;
			this._context.Application.Lock();
			try
			{
				IEnumerator enumerator = this._context.Application.GetEnumerator();
				while (enumerator.MoveNext())
				{
					dataRow = this.NewRow(this._requestData, "Trace_Application_State");
					string text = (string)enumerator.Current;
					dataRow["Trace_Application_Key"] = ((text != null) ? text : "<null>");
					object obj = this._context.Application[text];
					if (obj != null)
					{
						dataRow["Trace_Type"] = obj.GetType();
						dataRow["Trace_Value"] = obj.ToString();
					}
					else
					{
						dataRow["Trace_Type"] = "<null>";
						dataRow["Trace_Value"] = "<null>";
					}
					this.AddRow(this._requestData, "Trace_Application_State", dataRow);
				}
			}
			finally
			{
				this._context.Application.UnLock();
			}
			HttpCookieCollection httpCookieCollection = new HttpCookieCollection();
			this._context.Request.FillInCookiesCollection(httpCookieCollection, false);
			HttpCookie[] array = new HttpCookie[httpCookieCollection.Count];
			httpCookieCollection.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				dataRow = this.NewRow(this._requestData, "Trace_Request_Cookies_Collection");
				dataRow["Trace_Name"] = array[i].Name;
				if (array[i].Values.HasKeys())
				{
					NameValueCollection values = array[i].Values;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj2 in values)
					{
						string text = (string)obj2;
						stringBuilder.Append("(");
						stringBuilder.Append(text + "=");
						stringBuilder.Append(array[i][text] + ")  ");
					}
					dataRow["Trace_Value"] = stringBuilder.ToString();
				}
				else
				{
					dataRow["Trace_Value"] = array[i].Value;
				}
				int num = (array[i].Name == null) ? 0 : array[i].Name.Length;
				num += ((array[i].Value == null) ? 0 : array[i].Value.Length);
				dataRow["Trace_Size"] = num + 1;
				this.AddRow(this._requestData, "Trace_Request_Cookies_Collection", dataRow);
			}
			array = new HttpCookie[this._context.Response.Cookies.Count];
			this._context.Response.Cookies.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				dataRow = this.NewRow(this._requestData, "Trace_Response_Cookies_Collection");
				dataRow["Trace_Name"] = array[i].Name;
				if (array[i].Values.HasKeys())
				{
					NameValueCollection values2 = array[i].Values;
					StringBuilder stringBuilder2 = new StringBuilder();
					foreach (object obj3 in values2)
					{
						string text = (string)obj3;
						stringBuilder2.Append("(");
						stringBuilder2.Append(text + "=");
						stringBuilder2.Append(array[i][text] + ")  ");
					}
					dataRow["Trace_Value"] = stringBuilder2.ToString();
				}
				else
				{
					dataRow["Trace_Value"] = array[i].Value;
				}
				int num2 = (array[i].Name == null) ? 0 : array[i].Name.Length;
				num2 += ((array[i].Value == null) ? 0 : array[i].Value.Length);
				dataRow["Trace_Size"] = num2 + 1;
				this.AddRow(this._requestData, "Trace_Response_Cookies_Collection", dataRow);
			}
			HttpSessionState session = this._context.Session;
			if (session != null)
			{
				dataRow = this._requestData.Tables["Trace_Request"].Rows[0];
				try
				{
					dataRow["Trace_Session_Id"] = HttpUtility.UrlEncode(session.SessionID);
				}
				catch
				{
				}
				IEnumerator enumerator = session.GetEnumerator();
				while (enumerator.MoveNext())
				{
					dataRow = this.NewRow(this._requestData, "Trace_Session_State");
					string text = (string)enumerator.Current;
					dataRow["Trace_Session_Key"] = ((text != null) ? text : "<null>");
					object obj = session[text];
					if (obj != null)
					{
						dataRow["Trace_Type"] = obj.GetType();
						dataRow["Trace_Value"] = obj.ToString();
					}
					else
					{
						dataRow["Trace_Type"] = "<null>";
						dataRow["Trace_Value"] = "<null>";
					}
					this.AddRow(this._requestData, "Trace_Session_State", dataRow);
				}
			}
			this.ApplyTraceMode();
			this.OnTraceFinished(new TraceContextEventArgs(this._traceRecords));
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0002CF48 File Offset: 0x0002B148
		private void InitMaster()
		{
			DataSet dataSet = new DataSet();
			dataSet.Locale = CultureInfo.InvariantCulture;
			Type typeFromHandle = typeof(string);
			Type typeFromHandle2 = typeof(int);
			Type typeFromHandle3 = typeof(double);
			DataTable dataTable = dataSet.Tables.Add("Trace_Request");
			dataTable.Columns.Add("Trace_No", typeFromHandle2);
			dataTable.Columns.Add("Trace_Time_of_Request", typeFromHandle);
			dataTable.Columns.Add("Trace_Url", typeFromHandle);
			dataTable.Columns.Add("Trace_Request_Type", typeFromHandle);
			dataTable.Columns.Add("Trace_Status_Code", typeFromHandle2);
			dataTable.Columns.Add("Trace_Session_Id", typeFromHandle);
			dataTable.Columns.Add("Trace_Request_Encoding", typeFromHandle);
			dataTable.Columns.Add("Trace_Response_Encoding", typeFromHandle);
			dataTable = dataSet.Tables.Add("Trace_Control_Tree");
			dataTable.Columns.Add("Trace_Parent_Id", typeFromHandle);
			DataColumn[] array = new DataColumn[]
			{
				new DataColumn("Trace_Control_Id", typeFromHandle)
			};
			dataTable.Columns.Add(array[0]);
			dataTable.PrimaryKey = array;
			dataTable.Columns.Add("Trace_Type", typeFromHandle);
			dataTable.Columns.Add("Trace_Render_Size", typeFromHandle2);
			dataTable.Columns.Add("Trace_Viewstate_Size", typeFromHandle2);
			dataTable.Columns.Add("Trace_Controlstate_Size", typeFromHandle2);
			dataTable = dataSet.Tables.Add("Trace_Session_State");
			dataTable.Columns.Add("Trace_Session_Key", typeFromHandle);
			dataTable.Columns.Add("Trace_Type", typeFromHandle);
			dataTable.Columns.Add("Trace_Value", typeFromHandle);
			dataTable = dataSet.Tables.Add("Trace_Application_State");
			dataTable.Columns.Add("Trace_Application_Key", typeFromHandle);
			dataTable.Columns.Add("Trace_Type", typeFromHandle);
			dataTable.Columns.Add("Trace_Value", typeFromHandle);
			dataTable = dataSet.Tables.Add("Trace_Request_Cookies_Collection");
			dataTable.Columns.Add("Trace_Name", typeFromHandle);
			dataTable.Columns.Add("Trace_Value", typeFromHandle);
			dataTable.Columns.Add("Trace_Size", typeFromHandle2);
			dataTable = dataSet.Tables.Add("Trace_Response_Cookies_Collection");
			dataTable.Columns.Add("Trace_Name", typeFromHandle);
			dataTable.Columns.Add("Trace_Value", typeFromHandle);
			dataTable.Columns.Add("Trace_Size", typeFromHandle2);
			dataTable = dataSet.Tables.Add("Trace_Headers_Collection");
			dataTable.Columns.Add("Trace_Name", typeFromHandle);
			dataTable.Columns.Add("Trace_Value", typeFromHandle);
			dataTable = dataSet.Tables.Add("Trace_Response_Headers_Collection");
			dataTable.Columns.Add("Trace_Name", typeFromHandle);
			dataTable.Columns.Add("Trace_Value", typeFromHandle);
			dataTable = dataSet.Tables.Add("Trace_Form_Collection");
			dataTable.Columns.Add("Trace_Name", typeFromHandle);
			dataTable.Columns.Add("Trace_Value", typeFromHandle);
			dataTable = dataSet.Tables.Add("Trace_Querystring_Collection");
			dataTable.Columns.Add("Trace_Name", typeFromHandle);
			dataTable.Columns.Add("Trace_Value", typeFromHandle);
			dataTable = dataSet.Tables.Add("Trace_Trace_Information");
			dataTable.Columns.Add("Trace_Category", typeFromHandle);
			dataTable.Columns.Add("Trace_Warning", typeFromHandle);
			dataTable.Columns.Add("Trace_Message", typeFromHandle);
			dataTable.Columns.Add("Trace_From_First", typeFromHandle3);
			dataTable.Columns.Add("Trace_From_Last", typeFromHandle3);
			dataTable.Columns.Add("ErrorInfoMessage", typeFromHandle);
			dataTable.Columns.Add("ErrorInfoStack", typeFromHandle);
			dataTable = dataSet.Tables.Add("Trace_Server_Variables");
			dataTable.Columns.Add("Trace_Name", typeFromHandle);
			dataTable.Columns.Add("Trace_Value", typeFromHandle);
			TraceContext._masterRequest = dataSet;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0002D37E File Offset: 0x0002B57E
		private DataRow NewRow(DataSet ds, string table)
		{
			return ds.Tables[table].NewRow();
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0002D391 File Offset: 0x0002B591
		private void AddRow(DataSet ds, string table, DataRow row)
		{
			ds.Tables[table].Rows.Add(row);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0002D3AC File Offset: 0x0002B5AC
		private void InitRequest()
		{
			DataSet dataSet = TraceContext._masterRequest.Clone();
			DataRow dataRow = this.NewRow(dataSet, "Trace_Request");
			dataRow["Trace_Time_of_Request"] = this._context.Timestamp.ToString("G");
			string text = this._context.Request.RawUrl;
			int num = text.IndexOf("?", StringComparison.Ordinal);
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			dataRow["Trace_Url"] = text;
			dataRow["Trace_Request_Type"] = this._context.Request.HttpMethod;
			try
			{
				dataRow["Trace_Request_Encoding"] = this._context.Request.ContentEncoding.EncodingName;
			}
			catch
			{
			}
			if (this.TraceMode == TraceMode.SortByCategory)
			{
				dataSet.Tables["Trace_Trace_Information"].DefaultView.Sort = "Trace_Category";
			}
			this.AddRow(dataSet, "Trace_Request", dataRow);
			try
			{
				this.AddCollectionToRequestData(dataSet, "Trace_Headers_Collection", this._context.Request.Unvalidated.Headers);
			}
			catch
			{
			}
			ArrayList arrayList = this._context.Response.GenerateResponseHeaders(false);
			int num2 = (arrayList != null) ? arrayList.Count : 0;
			for (int i = 0; i < num2; i++)
			{
				HttpResponseHeader httpResponseHeader = (HttpResponseHeader)arrayList[i];
				dataRow = this.NewRow(dataSet, "Trace_Response_Headers_Collection");
				dataRow["Trace_Name"] = httpResponseHeader.Name;
				dataRow["Trace_Value"] = httpResponseHeader.Value;
				this.AddRow(dataSet, "Trace_Response_Headers_Collection", dataRow);
			}
			try
			{
				this.AddCollectionToRequestData(dataSet, "Trace_Form_Collection", this._context.Request.Unvalidated.Form);
			}
			catch
			{
			}
			try
			{
				this.AddCollectionToRequestData(dataSet, "Trace_Querystring_Collection", this._context.Request.Unvalidated.QueryString);
			}
			catch
			{
			}
			if (HttpRuntime.HasAppPathDiscoveryPermission())
			{
				this.AddCollectionToRequestData(dataSet, "Trace_Server_Variables", this._context.Request.ServerVariables);
			}
			this._requestData = dataSet;
			if (HttpRuntime.UseIntegratedPipeline)
			{
				this._context.Request.InsertEntityBody();
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0002D60C File Offset: 0x0002B80C
		private void AddCollectionToRequestData(DataSet requestData, string traceCollectionTitle, NameValueCollection collection)
		{
			if (collection != null)
			{
				string[] allKeys = collection.AllKeys;
				for (int i = 0; i < allKeys.Length; i++)
				{
					DataRow dataRow = this.NewRow(requestData, traceCollectionTitle);
					dataRow["Trace_Name"] = allKeys[i];
					dataRow["Trace_Value"] = collection[allKeys[i]];
					this.AddRow(requestData, traceCollectionTitle, dataRow);
				}
			}
		}

		// Token: 0x040005DC RID: 1500
		private static DataSet _masterRequest;

		// Token: 0x040005DD RID: 1501
		private static bool _writeToDiagnosticsTrace = false;

		// Token: 0x040005DE RID: 1502
		private static readonly object EventTraceFinished = new object();

		// Token: 0x040005DF RID: 1503
		private EventHandlerList _events = new EventHandlerList();

		// Token: 0x040005E0 RID: 1504
		private TraceMode _traceMode;

		// Token: 0x040005E1 RID: 1505
		private TraceEnable _isEnabled;

		// Token: 0x040005E2 RID: 1506
		private HttpContext _context;

		// Token: 0x040005E3 RID: 1507
		private DataSet _requestData;

		// Token: 0x040005E4 RID: 1508
		private long _firstTime;

		// Token: 0x040005E5 RID: 1509
		private long _lastTime;

		// Token: 0x040005E6 RID: 1510
		private int _uniqueIdCounter;

		// Token: 0x040005E7 RID: 1511
		private const string PAGEKEYNAME = "__PAGE";

		// Token: 0x040005E8 RID: 1512
		private const string NULLSTRING = "<null>";

		// Token: 0x040005E9 RID: 1513
		private const string NULLIDPREFIX = "__UnassignedID";

		// Token: 0x040005EA RID: 1514
		private ArrayList _traceRecords;

		// Token: 0x040005EB RID: 1515
		private bool _endDataCollected;

		// Token: 0x040005EC RID: 1516
		private bool _writing;
	}
}
