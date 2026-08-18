using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.Web.Profile
{
	// Token: 0x02000164 RID: 356
	public sealed class ProfileModule : IHttpModule
	{
		// Token: 0x0600141B RID: 5147 RVA: 0x000030B5 File Offset: 0x000012B5
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public ProfileModule()
		{
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600141C RID: 5148 RVA: 0x0003AEA2 File Offset: 0x000390A2
		// (remove) Token: 0x0600141D RID: 5149 RVA: 0x0003AEBB File Offset: 0x000390BB
		public event ProfileEventHandler Personalize
		{
			add
			{
				this._eventHandler = (ProfileEventHandler)Delegate.Combine(this._eventHandler, value);
			}
			remove
			{
				this._eventHandler = (ProfileEventHandler)Delegate.Remove(this._eventHandler, value);
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600141E RID: 5150 RVA: 0x0003AED4 File Offset: 0x000390D4
		// (remove) Token: 0x0600141F RID: 5151 RVA: 0x0003AEED File Offset: 0x000390ED
		public event ProfileMigrateEventHandler MigrateAnonymous
		{
			add
			{
				this._MigrateEventHandler = (ProfileMigrateEventHandler)Delegate.Combine(this._MigrateEventHandler, value);
			}
			remove
			{
				this._MigrateEventHandler = (ProfileMigrateEventHandler)Delegate.Remove(this._MigrateEventHandler, value);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001420 RID: 5152 RVA: 0x0003AF06 File Offset: 0x00039106
		// (remove) Token: 0x06001421 RID: 5153 RVA: 0x0003AF1F File Offset: 0x0003911F
		public event ProfileAutoSaveEventHandler ProfileAutoSaving
		{
			add
			{
				this._AutoSaveEventHandler = (ProfileAutoSaveEventHandler)Delegate.Combine(this._AutoSaveEventHandler, value);
			}
			remove
			{
				this._AutoSaveEventHandler = (ProfileAutoSaveEventHandler)Delegate.Remove(this._AutoSaveEventHandler, value);
			}
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00006164 File Offset: 0x00004364
		public void Dispose()
		{
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0003AF38 File Offset: 0x00039138
		public void Init(HttpApplication app)
		{
			if (ProfileManager.Enabled)
			{
				app.AcquireRequestState += this.OnEnter;
				if (ProfileManager.AutomaticSaveEnabled)
				{
					app.EndRequest += this.OnLeave;
				}
			}
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0003AF6C File Offset: 0x0003916C
		private void OnPersonalize(ProfileEventArgs e)
		{
			if (this._eventHandler != null)
			{
				this._eventHandler(this, e);
			}
			if (e.Profile != null)
			{
				e.Context._Profile = e.Profile;
				return;
			}
			e.Context._ProfileDelayLoad = true;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0003AFAC File Offset: 0x000391AC
		private void OnEnter(object source, EventArgs eventArgs)
		{
			HttpContext context = ((HttpApplication)source).Context;
			this.OnPersonalize(new ProfileEventArgs(context));
			if (context.Request.IsAuthenticated && !string.IsNullOrEmpty(context.Request.AnonymousID) && this._MigrateEventHandler != null)
			{
				ProfileMigrateEventArgs e = new ProfileMigrateEventArgs(context, context.Request.AnonymousID);
				this._MigrateEventHandler(this, e);
			}
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0003B018 File Offset: 0x00039218
		private void OnLeave(object source, EventArgs eventArgs)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (context._Profile == null || context._Profile == ProfileBase.SingletonInstance)
			{
				return;
			}
			if (this._AutoSaveEventHandler != null)
			{
				ProfileAutoSaveEventArgs profileAutoSaveEventArgs = new ProfileAutoSaveEventArgs(context);
				this._AutoSaveEventHandler(this, profileAutoSaveEventArgs);
				if (!profileAutoSaveEventArgs.ContinueWithProfileAutoSave)
				{
					return;
				}
			}
			context.Profile.Save();
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0003B07C File Offset: 0x0003927C
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		internal static void ParseDataFromDB(string[] names, string values, byte[] buf, SettingsPropertyValueCollection properties)
		{
			if (names == null || values == null || buf == null || properties == null)
			{
				return;
			}
			try
			{
				for (int i = 0; i < names.Length / 4; i++)
				{
					string name = names[i * 4];
					SettingsPropertyValue settingsPropertyValue = properties[name];
					if (settingsPropertyValue != null)
					{
						int num = int.Parse(names[i * 4 + 2], CultureInfo.InvariantCulture);
						int num2 = int.Parse(names[i * 4 + 3], CultureInfo.InvariantCulture);
						if (num2 == -1 && !settingsPropertyValue.Property.PropertyType.IsValueType)
						{
							settingsPropertyValue.PropertyValue = null;
							settingsPropertyValue.IsDirty = false;
							settingsPropertyValue.Deserialized = true;
						}
						if (names[i * 4 + 1] == "S" && num >= 0 && num2 > 0 && values.Length >= num + num2)
						{
							settingsPropertyValue.SerializedValue = values.Substring(num, num2);
						}
						if (names[i * 4 + 1] == "B" && num >= 0 && num2 > 0 && buf.Length >= num + num2)
						{
							byte[] array = new byte[num2];
							Buffer.BlockCopy(buf, num, array, 0, num2);
							settingsPropertyValue.SerializedValue = array;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0003B1A0 File Offset: 0x000393A0
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		internal static void PrepareDataForSaving(ref string allNames, ref string allValues, ref byte[] buf, bool binarySupported, SettingsPropertyValueCollection properties, bool userIsAuthenticated)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			MemoryStream memoryStream = binarySupported ? new MemoryStream() : null;
			try
			{
				try
				{
					bool flag = false;
					foreach (object obj in properties)
					{
						SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
						if (settingsPropertyValue.IsDirty)
						{
							if (!userIsAuthenticated)
							{
								bool flag2 = (bool)settingsPropertyValue.Property.Attributes["AllowAnonymous"];
								if (!flag2)
								{
									continue;
								}
							}
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return;
					}
					foreach (object obj2 in properties)
					{
						SettingsPropertyValue settingsPropertyValue2 = (SettingsPropertyValue)obj2;
						if (!userIsAuthenticated)
						{
							bool flag3 = (bool)settingsPropertyValue2.Property.Attributes["AllowAnonymous"];
							if (!flag3)
							{
								continue;
							}
						}
						if (settingsPropertyValue2.IsDirty || !settingsPropertyValue2.UsingDefaultValue)
						{
							int num = 0;
							string text = null;
							int num2;
							if (settingsPropertyValue2.Deserialized && settingsPropertyValue2.PropertyValue == null)
							{
								num2 = -1;
							}
							else
							{
								object obj3 = settingsPropertyValue2.SerializedValue;
								if (obj3 == null)
								{
									num2 = -1;
								}
								else
								{
									if (!(obj3 is string) && !binarySupported)
									{
										obj3 = Convert.ToBase64String((byte[])obj3);
									}
									if (obj3 is string)
									{
										text = (string)obj3;
										num2 = text.Length;
										num = stringBuilder2.Length;
									}
									else
									{
										byte[] array = (byte[])obj3;
										num = (int)memoryStream.Position;
										memoryStream.Write(array, 0, array.Length);
										memoryStream.Position = (long)(num + array.Length);
										num2 = array.Length;
									}
								}
							}
							stringBuilder.Append(string.Concat(new string[]
							{
								settingsPropertyValue2.Name,
								":",
								(text != null) ? "S" : "B",
								":",
								num.ToString(CultureInfo.InvariantCulture),
								":",
								num2.ToString(CultureInfo.InvariantCulture),
								":"
							}));
							if (text != null)
							{
								stringBuilder2.Append(text);
							}
						}
					}
					if (binarySupported)
					{
						buf = memoryStream.ToArray();
					}
				}
				finally
				{
					if (memoryStream != null)
					{
						memoryStream.Close();
					}
				}
			}
			catch
			{
				throw;
			}
			allNames = stringBuilder.ToString();
			allValues = stringBuilder2.ToString();
		}

		// Token: 0x0400151E RID: 5406
		private static object s_Lock = new object();

		// Token: 0x0400151F RID: 5407
		private ProfileEventHandler _eventHandler;

		// Token: 0x04001520 RID: 5408
		private ProfileMigrateEventHandler _MigrateEventHandler;

		// Token: 0x04001521 RID: 5409
		private ProfileAutoSaveEventHandler _AutoSaveEventHandler;
	}
}
