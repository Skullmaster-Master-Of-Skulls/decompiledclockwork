using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace Telerik.Web.UI
{
	// Token: 0x020012E0 RID: 4832
	public class XmlSchedulerProvider : SchedulerProviderBase
	{
		// Token: 0x0600CAD7 RID: 51927 RVA: 0x002D47DC File Offset: 0x002D29DC
		public XmlSchedulerProvider(string dataFileName, bool persistChanges)
		{
			this._dataFileName = dataFileName;
			this._doc = new XmlDocument();
			this._doc.Load(this._dataFileName);
			this._documentLoaded = true;
			this._nextID = this.ReadNextID();
			this.LoadResources();
			this._persistChanges = persistChanges;
		}

		// Token: 0x0600CAD8 RID: 51928 RVA: 0x002D4832 File Offset: 0x002D2A32
		public XmlSchedulerProvider(XmlDocument doc)
		{
			this._doc = doc;
			this._nextID = this.ReadNextID();
			this.LoadResources();
			this._persistChanges = false;
		}

		// Token: 0x0600CAD9 RID: 51929 RVA: 0x002D485C File Offset: 0x002D2A5C
		public XmlSchedulerProvider()
		{
			this._doc = new XmlDocument();
			this._nextID = 1;
			this._resources = new List<Resource>();
			this._doc.AppendChild(this._doc.CreateNode(XmlNodeType.Element, "Appointments", ""));
			this._persistChanges = false;
		}

		// Token: 0x0600CADA RID: 51930 RVA: 0x002D48B8 File Offset: 0x002D2AB8
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "XmlSchedulerProvider";
			}
			base.Initialize(name, config);
			this._dataFileName = config["fileName"];
			if (string.IsNullOrEmpty(this._dataFileName))
			{
				throw new ProviderException("Missing XML data file name. Please specify it with the fileName property.");
			}
			string value = config["persistChanges"];
			if (!string.IsNullOrEmpty(value))
			{
				if (!bool.TryParse(value, out this._persistChanges))
				{
					throw new ProviderException("Invalid value for PersistChanges attribute. Use 'True' or 'False'.");
				}
			}
			else
			{
				this._persistChanges = true;
			}
		}

		// Token: 0x0600CADB RID: 51931 RVA: 0x002D4948 File Offset: 0x002D2B48
		public override IEnumerable<Appointment> GetAppointments(RadScheduler owner)
		{
			this.EnsureFilePath(owner);
			this.LoadDataFile();
			List<Appointment> list = new List<Appointment>();
			foreach (object obj in this._doc.SelectNodes("//Appointments/Appointment"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				Appointment appointment = owner.CreateAppointment();
				list.Add(appointment);
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					string name;
					IList<Reminder> list2;
					if (owner.EnableDescriptionField && xmlNode2.Name == "Description")
					{
						appointment.Description = xmlNode2.InnerText;
					}
					else
						switch (name = xmlNode2.Name)
						{
						case "ID":
							appointment.ID = int.Parse(xmlNode2.InnerText);
							break;
						case "Subject":
							appointment.Subject = xmlNode2.InnerText;
							break;
						case "TimeZoneID":
							appointment.TimeZoneID = xmlNode2.InnerText;
							break;
						case "Start":
							appointment.Start = DateTime.Parse(xmlNode2.InnerText).ToUniversalTime();
							break;
						case "End":
							appointment.End = DateTime.Parse(xmlNode2.InnerText).ToUniversalTime();
							break;
						case "RecurrenceRule":
							appointment.RecurrenceRule = xmlNode2.InnerText;
							appointment.RecurrenceState = RecurrenceState.Master;
							break;
						case "RecurrenceParentID":
							appointment.RecurrenceParentID = int.Parse(xmlNode2.InnerText);
							appointment.RecurrenceState = RecurrenceState.Exception;
							break;
						case "Reminder":
							list2 = Reminder.TryParse(xmlNode2.InnerText);
							if (list2 != null)
							{
								appointment.Reminders.AddRange(list2);
							}
							break;
						case "Resources":
							this.LoadAppointmentResources(owner, appointment, xmlNode2);
							break;
						case "Attribute":
							appointment.Attributes.Add(xmlNode2.Attributes["Key"].Value, xmlNode2.Attributes["Value"].Value);
							break;
						}
				}
			}
			return list;
		}

		// Token: 0x0600CADC RID: 51932 RVA: 0x002D4C60 File Offset: 0x002D2E60
		public override void Insert(RadScheduler owner, Appointment appointmentToInsert)
		{
			this.EnsureFilePath(owner);
			this.LoadDataFile();
			appointmentToInsert.ID = this._nextID;
			XmlNode xmlNode = this._doc.SelectSingleNode("//Appointments");
			xmlNode.AppendChild(this.CreateAppointmentNode(owner, appointmentToInsert));
			this._nextID++;
			XmlNode xmlNode2 = this._doc.SelectSingleNode("//Appointments/NextID");
			if (xmlNode2 == null)
			{
				xmlNode2 = this._doc.CreateElement("NextID");
				xmlNode.AppendChild(xmlNode2);
			}
			xmlNode2.InnerText = this._nextID.ToString();
			this.SaveDataFile();
		}

		// Token: 0x0600CADD RID: 51933 RVA: 0x002D4D00 File Offset: 0x002D2F00
		public override void Update(RadScheduler owner, Appointment appointmentToUpdate)
		{
			this.EnsureFilePath(owner);
			this.LoadDataFile();
			if (appointmentToUpdate.ID == null)
			{
				this.Insert(owner, appointmentToUpdate);
			}
			XmlNode xmlNode = this._doc.SelectSingleNode("//Appointments/Appointment[ID=" + appointmentToUpdate.ID + "]");
			xmlNode.ParentNode.ReplaceChild(this.CreateAppointmentNode(owner, appointmentToUpdate), xmlNode);
			this.SaveDataFile();
		}

		// Token: 0x0600CADE RID: 51934 RVA: 0x002D4D68 File Offset: 0x002D2F68
		public override void Delete(RadScheduler owner, Appointment appointmentToDelete)
		{
			this.EnsureFilePath(owner);
			this.LoadDataFile();
			XmlNode xmlNode = this._doc.SelectSingleNode("//Appointments/Appointment[ID=" + appointmentToDelete.ID + "]");
			if (xmlNode != null)
			{
				xmlNode.ParentNode.RemoveChild(xmlNode);
				this.SaveDataFile();
			}
		}

		// Token: 0x0600CADF RID: 51935 RVA: 0x002D4DBC File Offset: 0x002D2FBC
		public override IEnumerable<ResourceType> GetResourceTypes(RadScheduler owner)
		{
			this.EnsureFilePath(owner);
			this.LoadDataFile();
			List<string> list = new List<string>();
			foreach (Resource resource in this._resources)
			{
				if (!list.Contains(resource.Type))
				{
					list.Add(resource.Type);
				}
			}
			List<ResourceType> list2 = new List<ResourceType>();
			foreach (string resourceTypeName in list)
			{
				list2.Add(new ResourceType(resourceTypeName));
			}
			return list2;
		}

		// Token: 0x0600CAE0 RID: 51936 RVA: 0x002D4E9C File Offset: 0x002D309C
		public override IEnumerable<Resource> GetResourcesByType(RadScheduler owner, string resourceType)
		{
			this.EnsureFilePath(owner);
			this.LoadDataFile();
			return this._resources.FindAll((Resource res) => res.Type == resourceType);
		}

		// Token: 0x0600CAE1 RID: 51937 RVA: 0x002D4EDC File Offset: 0x002D30DC
		private void LoadResources()
		{
			this._resources = new List<Resource>();
			foreach (object obj in this._doc.SelectNodes("//Appointments/Resources"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					Resource resource = new Resource();
					this._resources.Add(resource);
					resource.Type = xmlNode2.Name;
					foreach (object obj3 in xmlNode2.ChildNodes)
					{
						XmlNode xmlNode3 = (XmlNode)obj3;
						string name;
						if ((name = xmlNode3.Name) != null)
						{
							if (name == "Key")
							{
								resource.Key = xmlNode3.InnerText;
								continue;
							}
							if (name == "Text")
							{
								resource.Text = xmlNode3.InnerText;
								continue;
							}
						}
						resource.Attributes[xmlNode3.Name] = xmlNode3.InnerText;
					}
				}
			}
		}

		// Token: 0x0600CAE2 RID: 51938 RVA: 0x002D5084 File Offset: 0x002D3284
		private void LoadAppointmentResources(RadScheduler owner, Appointment appointment, XmlNode appointmentResourcesNode)
		{
			foreach (object obj in appointmentResourcesNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string name = xmlNode.Name;
				string value = xmlNode.Attributes["Key"].Value;
				Resource resource = this.GetResource(owner, name, value);
				if (!(resource != null))
				{
					throw new Exception(string.Format("Cannot find resource of type '{0}' with Key={1} for appointment with ID={2}.", name, value, appointment.ID));
				}
				appointment.Resources.Add(resource);
			}
		}

		// Token: 0x0600CAE3 RID: 51939 RVA: 0x002D515C File Offset: 0x002D335C
		private Resource GetResource(RadScheduler owner, string type, object key)
		{
			List<Resource> list = (List<Resource>)this.GetResourcesByType(owner, type);
			return list.Find((Resource res) => key != null && res.Key.Equals(key));
		}

		// Token: 0x0600CAE4 RID: 51940 RVA: 0x002D5198 File Offset: 0x002D3398
		private XmlNode CreateAppointmentNode(RadScheduler owner, Appointment appointment)
		{
			XmlNode xmlNode = this._doc.CreateNode(XmlNodeType.Element, "Appointment", string.Empty);
			XmlNode xmlNode2 = this._doc.CreateNode(XmlNodeType.Element, "ID", string.Empty);
			xmlNode2.InnerText = appointment.ID.ToString();
			xmlNode.AppendChild(xmlNode2);
			XmlNode xmlNode3 = this._doc.CreateNode(XmlNodeType.Element, "Subject", string.Empty);
			xmlNode3.InnerText = appointment.Subject;
			xmlNode.AppendChild(xmlNode3);
			if (owner.EnableDescriptionField)
			{
				XmlNode xmlNode4 = this._doc.CreateNode(XmlNodeType.Element, "Description", string.Empty);
				xmlNode4.InnerText = appointment.Description;
				xmlNode.AppendChild(xmlNode4);
			}
			XmlNode xmlNode5 = this._doc.CreateNode(XmlNodeType.Element, "Start", string.Empty);
			xmlNode5.InnerText = appointment.Start.ToUniversalTime().ToString("yyyy-MM-ddTHH:mmZ", CultureInfo.InvariantCulture);
			xmlNode.AppendChild(xmlNode5);
			XmlNode xmlNode6 = this._doc.CreateNode(XmlNodeType.Element, "End", string.Empty);
			xmlNode6.InnerText = appointment.End.ToUniversalTime().ToString("yyyy-MM-ddTHH:mmZ", CultureInfo.InvariantCulture);
			xmlNode.AppendChild(xmlNode6);
			if (!string.IsNullOrEmpty(appointment.TimeZoneID))
			{
				XmlNode xmlNode7 = this._doc.CreateNode(XmlNodeType.Element, "TimeZoneID", string.Empty);
				xmlNode7.InnerText = appointment.TimeZoneID;
				xmlNode.AppendChild(xmlNode7);
			}
			if (!string.IsNullOrEmpty(appointment.RecurrenceRule))
			{
				XmlNode xmlNode8 = this._doc.CreateNode(XmlNodeType.Element, "RecurrenceRule", string.Empty);
				xmlNode.AppendChild(xmlNode8);
				XmlNode xmlNode9 = this._doc.CreateNode(XmlNodeType.CDATA, string.Empty, string.Empty);
				xmlNode8.AppendChild(xmlNode9);
				xmlNode9.InnerText = appointment.RecurrenceRule;
			}
			if (appointment.RecurrenceState == RecurrenceState.Exception)
			{
				XmlNode xmlNode10 = this._doc.CreateNode(XmlNodeType.Element, "RecurrenceParentID", string.Empty);
				xmlNode10.InnerText = appointment.RecurrenceParentID.ToString();
				xmlNode.AppendChild(xmlNode10);
			}
			if (appointment.Reminders.Count > 0)
			{
				XmlNode xmlNode11 = this._doc.CreateNode(XmlNodeType.Element, "Reminder", string.Empty);
				xmlNode.AppendChild(xmlNode11);
				XmlNode xmlNode12 = this._doc.CreateNode(XmlNodeType.CDATA, string.Empty, string.Empty);
				xmlNode11.AppendChild(xmlNode12);
				xmlNode12.InnerText = appointment.Reminders.ToString().Trim();
			}
			this.SaveAppointmentResources(appointment, xmlNode);
			this.SaveAppointmentAttributes(appointment, xmlNode);
			return xmlNode;
		}

		// Token: 0x0600CAE5 RID: 51941 RVA: 0x002D542C File Offset: 0x002D362C
		[MethodImpl(MethodImplOptions.Synchronized)]
		private void LoadDataFile()
		{
			if (string.IsNullOrEmpty(this._dataFileName))
			{
				return;
			}
			if (this._documentLoaded && !this._persistChanges)
			{
				return;
			}
			this._doc.Load(this._dataFileName);
			this._documentLoaded = true;
			this._nextID = this.ReadNextID();
			this.LoadResources();
		}

		// Token: 0x0600CAE6 RID: 51942 RVA: 0x002D5482 File Offset: 0x002D3682
		[MethodImpl(MethodImplOptions.Synchronized)]
		private void SaveDataFile()
		{
			if (this._persistChanges && !string.IsNullOrEmpty(this._dataFileName))
			{
				this._doc.Save(this._dataFileName);
			}
		}

		// Token: 0x0600CAE7 RID: 51943 RVA: 0x002D54AC File Offset: 0x002D36AC
		private void EnsureFilePath(Control owner)
		{
			if (string.IsNullOrEmpty(this._dataFileName))
			{
				return;
			}
			if (!this._dataFileName.StartsWith("~") && File.Exists(this._dataFileName))
			{
				return;
			}
			if (owner.Page != null)
			{
				this._dataFileName = owner.Page.MapPath(this._dataFileName);
				return;
			}
			if (HttpContext.Current != null)
			{
				this._dataFileName = HttpContext.Current.Request.MapPath(this._dataFileName);
			}
		}

		// Token: 0x0600CAE8 RID: 51944 RVA: 0x002D552C File Offset: 0x002D372C
		private void SaveAppointmentResources(Appointment appointment, XmlNode appointmentNode)
		{
			if (appointment.Resources.Count == 0)
			{
				return;
			}
			XmlNode xmlNode = this._doc.CreateNode(XmlNodeType.Element, "Resources", string.Empty);
			appointmentNode.AppendChild(xmlNode);
			foreach (object obj in appointment.Resources)
			{
				Resource resource = (Resource)obj;
				XmlNode xmlNode2 = this._doc.CreateNode(XmlNodeType.Element, resource.Type, string.Empty);
				xmlNode.AppendChild(xmlNode2);
				XmlAttribute xmlAttribute = this._doc.CreateAttribute("Key");
				xmlNode2.Attributes.Append(xmlAttribute);
				xmlAttribute.InnerText = resource.Key.ToString();
			}
		}

		// Token: 0x0600CAE9 RID: 51945 RVA: 0x002D5604 File Offset: 0x002D3804
		private void SaveAppointmentAttributes(Appointment appointment, XmlNode appointmentNode)
		{
			foreach (object obj in appointment.Attributes.Keys)
			{
				string text = (string)obj;
				if (!string.IsNullOrEmpty(appointment.Attributes[text]))
				{
					XmlNode xmlNode = this._doc.CreateNode(XmlNodeType.Element, "Attribute", string.Empty);
					appointmentNode.AppendChild(xmlNode);
					XmlAttribute xmlAttribute = this._doc.CreateAttribute("Key");
					xmlNode.Attributes.Append(xmlAttribute);
					xmlAttribute.InnerText = text;
					XmlAttribute xmlAttribute2 = this._doc.CreateAttribute("Value");
					xmlNode.Attributes.Append(xmlAttribute2);
					xmlAttribute2.InnerText = appointment.Attributes[text];
				}
			}
		}

		// Token: 0x0600CAEA RID: 51946 RVA: 0x002D56F0 File Offset: 0x002D38F0
		private int ReadNextID()
		{
			XmlNode xmlNode = this._doc.SelectSingleNode("//Appointments/NextID");
			if (xmlNode == null)
			{
				return 1;
			}
			return int.Parse(xmlNode.InnerText);
		}

		// Token: 0x0400353D RID: 13629
		private const string DateFormatString = "yyyy-MM-ddTHH:mmZ";

		// Token: 0x0400353E RID: 13630
		private readonly XmlDocument _doc;

		// Token: 0x0400353F RID: 13631
		private string _dataFileName;

		// Token: 0x04003540 RID: 13632
		private int _nextID;

		// Token: 0x04003541 RID: 13633
		private List<Resource> _resources;

		// Token: 0x04003542 RID: 13634
		private bool _documentLoaded;

		// Token: 0x04003543 RID: 13635
		private bool _persistChanges;
	}
}
