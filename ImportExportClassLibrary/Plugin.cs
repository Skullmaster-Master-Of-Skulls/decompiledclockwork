using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Reflection;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ImportExportClassLibrary
{
	// Token: 0x02000041 RID: 65
	public class Plugin
	{
		// Token: 0x0600025F RID: 607 RVA: 0x000184CC File Offset: 0x000174CC
		public Plugin(string pluginFilename, string pluginsPath, DataRow pluginDataRow)
		{
			this.PluginType = this.GetPluginType(pluginFilename);
			if (this.PluginType == PluginType.Kiosk && pluginDataRow[1].ToString().Trim().ToLower().Equals("kioskimport2"))
			{
				this.PluginType = PluginType.Kiosk2;
			}
			this.PluginFilename = pluginFilename;
			this.PluginsPath = pluginsPath;
			this.PluginDataRow = pluginDataRow;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00018533 File Offset: 0x00017533
		public string Description
		{
			get
			{
				if (this.PluginDataRow != null)
				{
					return this.PluginDataRow[1].ToString() + " : " + this.PluginDataRow[2].ToString();
				}
				return "";
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0001856F File Offset: 0x0001756F
		public string ConnectionString
		{
			get
			{
				if (this.PluginDataRow != null)
				{
					return this.PluginDataRow[3].ToString();
				}
				return "";
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00018590 File Offset: 0x00017590
		public string SelectCMD
		{
			get
			{
				if (this.PluginDataRow != null)
				{
					return this.PluginDataRow[4].ToString();
				}
				return "";
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000185B1 File Offset: 0x000175B1
		public string UpdateCMD
		{
			get
			{
				if (this.PluginDataRow != null)
				{
					return this.PluginDataRow[5].ToString();
				}
				return "";
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000185D4 File Offset: 0x000175D4
		private PluginType GetPluginType(string pluginFilename)
		{
			string text = Path.GetFileNameWithoutExtension(pluginFilename);
			text = text.Trim().ToLower();
			if (text.IndexOf("notetaking") == 0)
			{
				return PluginType.Notetakers;
			}
			if (text.IndexOf("exambooking") == 0)
			{
				return PluginType.ExamBooking;
			}
			if (text.IndexOf("lookupcourses") == 0)
			{
				return PluginType.LookupCourses;
			}
			if (text.IndexOf("kioskimport") == 0)
			{
				return PluginType.Kiosk;
			}
			if (text.IndexOf("kioskimport2") == 0)
			{
				return PluginType.Kiosk2;
			}
			return PluginType.Unknown;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00018640 File Offset: 0x00017640
		public bool LoadPlugin()
		{
			return this.LoadPlugin(new object[0]);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00018650 File Offset: 0x00017650
		public bool LoadPlugin(object[] args)
		{
			bool result;
			try
			{
				this.assembly = Assembly.LoadFile(Path.Combine(this.PluginsPath, this.PluginFilename));
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.PluginFilename);
				Type type = this.assembly.GetType(fileNameWithoutExtension + "." + fileNameWithoutExtension);
				object obj = Activator.CreateInstance(type, args);
				if (obj is ImportODBC)
				{
					this.ImportODBC = (ImportODBC)obj;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				result = false;
			}
			return result;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000186E4 File Offset: 0x000176E4
		public static ArrayList LoadPlugins(UnivDataAdapter da, string pluginsPath)
		{
			da.SelectCommand.CommandText = "SELECT plugintypeid,pluginfilename,description,connstring,selectcmd,updatecmd,pluginparameters FROM plugins WHERE NOT connstring='' AND NOT selectcmd=''";
			DataTable dataTable = new DataTable("plugins");
			da.Fill(dataTable);
			int count = dataTable.Rows.Count;
			ArrayList arrayList = new ArrayList();
			if (Directory.Exists(pluginsPath))
			{
				string[] files = Directory.GetFiles(pluginsPath);
				foreach (string path in files)
				{
					List<DataRow> pluginDataRows = Plugin.GetPluginDataRows(dataTable, Path.GetFileName(path));
					foreach (DataRow pluginDataRow in pluginDataRows)
					{
						Plugin value = new Plugin(Path.GetFileName(path), pluginsPath, pluginDataRow);
						arrayList.Add(value);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000187B8 File Offset: 0x000177B8
		private static DataRow GetPluginDataRow(DataTable pluginsTable, string pluginFilenameNoDll)
		{
			string text = Path.GetFileNameWithoutExtension(pluginFilenameNoDll).ToLower().Trim();
			foreach (object obj in pluginsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text2 = ((string)dataRow[1]).ToLower();
				if (text2.IndexOf(text) == 0)
				{
					return dataRow;
				}
				if (text2.IndexOf(text + "import") == 0)
				{
					return dataRow;
				}
				if ((text2 + "import").IndexOf(text) == 0)
				{
					return dataRow;
				}
				if ((text2 + "import2").IndexOf(text) == 0)
				{
					return dataRow;
				}
			}
			return null;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0001888C File Offset: 0x0001788C
		private static List<DataRow> GetPluginDataRows(DataTable pluginsTable, string pluginFilenameNoDll)
		{
			List<DataRow> list = new List<DataRow>();
			string text = Path.GetFileNameWithoutExtension(pluginFilenameNoDll).ToLower().Trim();
			foreach (object obj in pluginsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text2 = ((string)dataRow[1]).ToLower();
				if (text2.IndexOf(text) == 0)
				{
					list.Add(dataRow);
				}
				else if (text2.IndexOf(text + "import") == 0)
				{
					list.Add(dataRow);
				}
				else if ((text2 + "import").IndexOf(text) == 0)
				{
					list.Add(dataRow);
				}
				else if ((text2 + "import2").IndexOf(text) == 0)
				{
					list.Add(dataRow);
				}
			}
			return list;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0001897C File Offset: 0x0001797C
		public static Plugin GetPlugin(ArrayList plugins, PluginType pluginType)
		{
			foreach (object obj in plugins)
			{
				Plugin plugin = (Plugin)obj;
				if (plugin.PluginType == pluginType)
				{
					return plugin;
				}
			}
			return null;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000189DC File Offset: 0x000179DC
		public NameValueCollection GetPluginParameters(TripleDESEncryptionClass tripleDES)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			if (this.PluginDataRow[6] != DBNull.Value)
			{
				byte[] inputInBytes = (byte[])this.PluginDataRow[6];
				string text = tripleDES.Decrypt(inputInBytes);
				string[] array = text.Split(new char[]
				{
					'`'
				});
				foreach (string text2 in array)
				{
					string[] array3 = text2.Split(new char[]
					{
						'='
					});
					if (array3.Length == 2)
					{
						nameValueCollection.Add(array3[0], array3[1]);
					}
				}
			}
			return nameValueCollection;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00018A80 File Offset: 0x00017A80
		public static NameValueCollection GetPluginParameters(byte[] Parameters, TripleDESEncryptionClass tripleDES)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			if (Parameters != null)
			{
				string text = tripleDES.Decrypt(Parameters);
				string[] array = text.Split(new char[]
				{
					'`'
				});
				foreach (string text2 in array)
				{
					string[] array3 = text2.Split(new char[]
					{
						'='
					});
					if (array3.Length == 2)
					{
						nameValueCollection.Add(array3[0], array3[1]);
					}
				}
			}
			return nameValueCollection;
		}

		// Token: 0x04000139 RID: 313
		public PluginType PluginType;

		// Token: 0x0400013A RID: 314
		public Assembly assembly;

		// Token: 0x0400013B RID: 315
		public ImportODBC ImportODBC;

		// Token: 0x0400013C RID: 316
		public string PluginFilename;

		// Token: 0x0400013D RID: 317
		public string PluginsPath;

		// Token: 0x0400013E RID: 318
		public DataRow PluginDataRow;
	}
}
