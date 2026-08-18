using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Licensing;
using Telerik.Web.Extensions;
using Telerik.Web.UI.Common.SerializeJS;
using Telerik.Web.UI.Gantt;

namespace Telerik.Web.UI
{
	// Token: 0x0200032F RID: 815
	[ToolboxData("<{0}:RadGantt runat=\"server\"></{0}:RadGantt>")]
	[ToolboxBitmap(typeof(RadGantt), "Telerik.Web.UI.Gantt.png")]
	[RequiredScript(typeof(jQueryPlugins), 1)]
	[RequiredScript(typeof(Html5Gantt), 2)]
	[RequiredScript(typeof(MaterialRipple))]
	[ClientScriptResource("Telerik.Web.UI.RadGantt", "Telerik.Web.UI.Gantt.RadGanttScripts.js", LoadOrder = 3)]
	[EmbeddedSkin("Gantt", typeof(RadGantt))]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("Gantt", "Default", typeof(RadGantt))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadGantt))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[Designer("Telerik.Web.Design.RadGanttDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[TelerikToolboxCategory("Calendar, Scheduler and Gantt")]
	public class RadGantt : RadDataBoundControl, INamingContainer, IGantt, ILocalizableControl, ICallbackEventHandler, IPostBackEventHandler
	{
		// Token: 0x06001B19 RID: 6937 RVA: 0x0005704F File Offset: 0x0005524F
		private void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this, "Lite"));
			writer.Write(string.Format("<div id='RadGantt1' class='RadGantt RadGantt_{0} radSkin_{0}' style='height: 500px; width: 1000px;'>\r\n                <div class='rgtToolbar rgtHeader' style='overflow: hidden; height: 26px;'>\r\n                    <ul class='radToolbar rgtActions'>\r\n                        <li class='radButton' data-action='add'><span class='radIcon radIconPlus'></span>Add Task</li>\r\n                    </ul>\r\n                    <ul class='radToolbar rgtViews'>\r\n                        <li class='radStateDefault rgtViewButton-day radStateSelected'><a href='#' class='radButton'>Day</a></li>\r\n                        <li class='radStateDefault rgtViewButton-week' data-name='week'><a href='#' class='radButton'>Week</a></li>\r\n                        <li class='radStateDefault rgtViewButton-month' data-name='month'><a href='#' class='radButton'>Month</a></li>\r\n                    </ul>\r\n                </div>\r\n                <div class='rgtTreelistWrapper' style='width: 387px; height: 438px; overflow: hidden;'>\r\n                    <div data-role='ganttlist' class='radGrid rgtTreelist'>\r\n                        <div class='radGridHeader'>\r\n                            <div class='radGridHeaderWrap'>\r\n                                <table style='min-width: 500px;' cellpadding='0' cellspacing='0'>\r\n                                    <colgroup>\r\n                                        <col style='width: 50px;'><col><col><col><col>\r\n                                    </colgroup>\r\n                                    <thead>\r\n                                        <tr>\r\n                                            <th data-field='id' data-title='ID' class='radHeader'>ID</th>\r\n                                            <th data-field='title' data-title='Title' class='radHeader'><a class='k-link' href='#'>Title</a></th>\r\n                                            <th data-field='start' data-title='Start Time' class='radHeader'><a class='k-link' href='#'>Start Time</a></th>\r\n                                            <th data-field='end' data-title='End Time' class='radHeader'><a class='k-link' href='#'>End Time</a></th>\r\n                                            <th data-field='percentComplete' data-title='Percent Complete' class='radHeader'><a class='k-link' href='#'>Percent Complete</a></th>\r\n                                        </tr>\r\n                                    </thead>\r\n                                </table>\r\n                            </div>\r\n                        </div>\r\n                        <div class='rgtTreelistContent radGridContent' data-role='touch' style='height: 381px;'>\r\n                            <table style='min-width: 500px;' cellpadding='0' cellspacing='0'>\r\n                                <colgroup>\r\n                                    <col style='width: 50px;'><col><col><col><col>\r\n                                </colgroup>\r\n                                <tbody>\r\n                                    <tr data-uid='63d22362-0cc6-4ce7-bde9-c687b6cb2081' data-level='0' class='rgtTreelistGroup'>\r\n                                        <td><span>109</span></td>\r\n                                        <td><span class='radIcon radIconCollapse'></span><span>Task 1</span></td>\r\n                                        <td><span>06/13/2014 09:00 </span></td>\r\n                                        <td><span>06/13/2014 12:00 </span></td>\r\n                                        <td><span>30.00 %</span></td>\r\n                                    </tr>\r\n                                    <tr data-uid='17d35e05-3cc1-47bc-8f9b-a1283e26c08e' data-level='1' class='radAlt'>\r\n                                        <td><span>108</span></td>\r\n                                        <td><span class='radIcon radIconNone'></span><span class='radIcon radIconNone'></span><span>Task 1.1</span></td>\r\n                                        <td><span>06/13/2014 09:00 </span></td>\r\n                                        <td><span>06/13/2014 10:00 </span></td>\r\n                                        <td><span>29.00 %</span></td>\r\n                                    </tr>\r\n                                    <tr data-uid='f9a325c3-e255-4290-a0ca-ac6db4eea646' data-level='1' class=''>\r\n                                        <td><span>110</span></td>\r\n                                        <td><span class='radIcon radIconNone'></span><span class='radIcon radIconNone'></span><span>Task 1.2</span></td>\r\n                                        <td><span>06/13/2014 11:00 </span></td>\r\n                                        <td><span>06/13/2014 12:00 </span></td>\r\n                                        <td><span>31.00 %</span></td>\r\n                                    </tr>\r\n                                </tbody>\r\n                            </table>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n                <div class='rgtSplitbar' style='height: 438px;'>\r\n                    <div class='radIcon radResizeHandle'></div>\r\n                </div>\r\n                <div class='rgtTimelineWrapper' style='height: 438px; width: 606px; overflow: hidden;'>\r\n                    <div data-role='gantttimeline' class='radGrid rgtTimeline' tabindex='0'>\r\n                        <div class='radGridHeader'>\r\n                            <div class='radGridHeaderWrap'>\r\n                                <table style='width: 900px;' cellpadding='0' cellspacing='0'>\r\n                                    <colgroup>\r\n                                        <col><col><col><col><col><col><col><col><col>\r\n                                    </colgroup>\r\n                                    <thead>\r\n                                        <tr>\r\n                                            <th colspan='9' class='radHeader'>Fri 6/13</th>\r\n                                        </tr>\r\n                                        <tr>\r\n                                            <th colspan='1' class='radHeader'>8:00 AM</th>\r\n                                            <th colspan='1' class='radHeader'>9:00 AM</th>\r\n                                            <th colspan='1' class='radHeader'>10:00 AM</th>\r\n                                            <th colspan='1' class='radHeader'>11:00 AM</th>\r\n                                            <th colspan='1' class='radHeader'>12:00 PM</th>\r\n                                            <th colspan='1' class='radHeader'>1:00 PM</th>\r\n                                            <th colspan='1' class='radHeader'>2:00 PM</th>\r\n                                            <th colspan='1' class='radHeader'>3:00 PM</th>\r\n                                            <th colspan='1' class='radHeader'>4:00 PM</th>\r\n                                        </tr>\r\n                                    </thead>\r\n                                </table>\r\n                            </div>\r\n                        </div>\r\n                        <div class='rgtTimelineContent radGridContent' style='height: 381px;'>\r\n                            <div class='rgtTables'>\r\n                                <table class='radFauxRows' style='width: 900px;' cellpadding='0' cellspacing='0'>\r\n                                    <colgroup>\r\n                                        <col>\r\n                                    </colgroup>\r\n                                    <tbody>\r\n                                        <tr>\r\n                                            <td>&nbsp;</td>\r\n                                        </tr>\r\n                                        <tr class='radAlt'>\r\n                                            <td>&nbsp;</td>\r\n                                        </tr>\r\n                                        <tr>\r\n                                            <td>&nbsp;</td>\r\n                                        </tr>\r\n                                    </tbody>\r\n                                </table>\r\n                                <table class='radFauxColumns' style='width: 900px; height: 84px;' cellpadding='0' cellspacing='0'>\r\n                                    <colgroup>\r\n                                        <col><col><col><col><col><col><col><col><col>\r\n                                    </colgroup>\r\n                                    <tbody>\r\n                                        <tr>\r\n                                            <td>&nbsp;</td>\r\n                                            <td>&nbsp;</td>\r\n                                            <td>&nbsp;</td>\r\n                                            <td>&nbsp;</td>\r\n                                            <td>&nbsp;</td>\r\n                                            <td>&nbsp;</td>\r\n                                            <td>&nbsp;</td>\r\n                                            <td>&nbsp;</td>\r\n                                            <td>&nbsp;</td>\r\n                                        </tr>\r\n                                    </tbody>\r\n                                </table>\r\n                                <table class='rgtTasks' style='width: 900px;' cellpadding='0' cellspacing='0'>\r\n                                    <colgroup>\r\n                                        <col>\r\n                                    </colgroup>\r\n                                    <tbody>\r\n                                        <tr>\r\n                                            <td>\r\n                                                <div class='rgtTaskWrap' style='left: 100px;'>\r\n                                                    <div class='rgtTask rgtTaskSummary' data-uid='63d22362-0cc6-4ce7-bde9-c687b6cb2081' style='width: 300px;'>\r\n                                                        <div class='rgtProgress' style='width: 90px;'>\r\n                                                            <div class='rgtComplete' style='width: 300px;'></div>\r\n                                                        </div>\r\n                                                    </div>\r\n                                                    <div class='rgtTaskDot rgtTaskStart'></div>\r\n                                                    <div class='rgtTaskDot rgtTaskEnd'></div>\r\n                                                </div>\r\n                                            </td>\r\n                                        </tr>\r\n                                        <tr>\r\n                                            <td>\r\n                                                <div class='rgtTaskWrap' style='left: 100px;'>\r\n                                                    <div class='rgtTask rgtTaskSingle' data-uid='17d35e05-3cc1-47bc-8f9b-a1283e26c08e' style='width: 98px;'>\r\n                                                        <div class='rgtTaskComplete' style='width: 29px;'></div>\r\n                                                        <div class='rgtTaskContent'>\r\n                                                            <div class='rgtTaskTemplate'>Task 1.1</div>\r\n                                                            <span class='rgtTaskActions'><a class='radButton rgtTaskDelete' href='#'><span class='radIcon radIconDelete'></span></a></span><span class='radResizeHandle radResizeW'></span><span class='radResizeHandle radResizeE'></span></div>\r\n                                                    </div>\r\n                                                    <div class='rgtTaskDot rgtTaskStart'></div>\r\n                                                    <div class='rgtTaskDot rgtTaskEnd'></div>\r\n                                                    <div class='rgtDragHandle' style='left: 29px;'></div>\r\n                                                </div>\r\n                                            </td>\r\n                                        </tr>\r\n                                        <tr>\r\n                                            <td>\r\n                                                <div class='rgtTaskWrap' style='left: 300px;'>\r\n                                                    <div class='rgtTask rgtTaskSingle' data-uid='f9a325c3-e255-4290-a0ca-ac6db4eea646' style='width: 98px;'>\r\n                                                        <div class='rgtTaskComplete' style='width: 31px;'></div>\r\n                                                        <div class='rgtTaskContent'>\r\n                                                            <div class='rgtTaskTemplate'>Task 1.2</div>\r\n                                                            <span class='rgtTaskActions'><a class='radButton rgtTaskDelete' href='#'><span class='radIcon radIconDelete'></span></a></span><span class='radResizeHandle radResizeW'></span><span class='radResizeHandle radResizeE'></span></div>\r\n                                                    </div>\r\n                                                    <div class='rgtTaskDot rgtTaskStart'></div>\r\n                                                    <div class='rgtTaskDot rgtTaskEnd'></div>\r\n                                                    <div class='rgtDragHandle' style='left: 31px;'></div>\r\n                                                </div>\r\n                                            </td>\r\n                                        </tr>\r\n                                    </tbody>\r\n                                </table>\r\n                            </div>\r\n                            <div class='rgtDependencies'>\r\n                                <div class='radLine radLineH' data-uid='e8ab15bf-4c1f-414c-8e64-401e3802c02b' style='left: 200px; top: 41px; width: 14px;'></div>\r\n                                <div class='radLine radLineV' data-uid='e8ab15bf-4c1f-414c-8e64-401e3802c02b' style='left: 214px; top: 41px; height: 28px;'></div>\r\n                                <div class='radLine radLineH' data-uid='e8ab15bf-4c1f-414c-8e64-401e3802c02b' style='left: 214px; top: 69px; width: 85px;'><span class='radArrowE'></span></div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n                <div class='rgtToolbar rgtFooter' style='overflow: hidden; height: 26px;'>\r\n                    <ul class='radToolbar rgtActions'>\r\n                        <li class='radButton' data-action='add'><span class='radIcon radIconPlus'></span>Add Task</li>\r\n                    </ul>\r\n                </div>\r\n            </div>", this.Skin));
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00057078 File Offset: 0x00055278
		protected virtual void OnColumnCreating(ColumnCreatingEventArgs e)
		{
			Delegate @delegate = base.Events[RadGantt.columnCreatingEvent];
			if (@delegate != null)
			{
				((ColumnCreatingEventHandler)@delegate)(this, e);
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000570A8 File Offset: 0x000552A8
		protected virtual void OnColumnCreated(ColumnCreatedEventArgs e)
		{
			Delegate @delegate = base.Events[RadGantt.columnCreatedEvent];
			if (@delegate != null)
			{
				((ColumnCreatedEventHandler)@delegate)(this, e);
			}
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000570D8 File Offset: 0x000552D8
		protected virtual void OnTaskInsert(TaskEventArgs e)
		{
			Delegate @delegate = base.Events[RadGantt.taskInsertEvent];
			if (@delegate != null)
			{
				((TaskInsertEventHandler)@delegate)(this, e);
			}
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00057108 File Offset: 0x00055308
		protected virtual void OnTaskUpdate(TaskEventArgs e)
		{
			Delegate @delegate = base.Events[RadGantt.taskUpdateEvent];
			if (@delegate != null)
			{
				((TaskUpdateEventHandler)@delegate)(this, e);
			}
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00057138 File Offset: 0x00055338
		protected virtual void OnTaskDelete(TaskEventArgs e)
		{
			Delegate @delegate = base.Events[RadGantt.taskDeleteEvent];
			if (@delegate != null)
			{
				((TaskDeleteEventHandler)@delegate)(this, e);
			}
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00057168 File Offset: 0x00055368
		protected virtual void OnDependencyInsert(DependencyEventArgs e)
		{
			Delegate @delegate = base.Events[RadGantt.dependencyInsertEvent];
			if (@delegate != null)
			{
				((DependencyInsertEventHandler)@delegate)(this, e);
			}
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00057198 File Offset: 0x00055398
		protected virtual void OnDependencyDelete(DependencyEventArgs e)
		{
			Delegate @delegate = base.Events[RadGantt.dependencyDeleteEvent];
			if (@delegate != null)
			{
				((DependencyDeleteEventHandler)@delegate)(this, e);
			}
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000571C8 File Offset: 0x000553C8
		protected virtual void OnAssignmentInsert(AssignmentEventArgs e)
		{
			Delegate @delegate = base.Events[RadGantt.assignmentInsertEvent];
			if (@delegate != null)
			{
				((AssignmentInsertEventHandler)@delegate)(this, e);
			}
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000571F8 File Offset: 0x000553F8
		protected virtual void OnAssignmentUpdate(AssignmentEventArgs e)
		{
			Delegate @delegate = base.Events[RadGantt.assignmentUpdateEvent];
			if (@delegate != null)
			{
				((AssignmentUpdateEventHandler)@delegate)(this, e);
			}
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x00057228 File Offset: 0x00055428
		protected virtual void OnAssignmentDelete(AssignmentEventArgs e)
		{
			Delegate @delegate = base.Events[RadGantt.assignmentDeleteEvent];
			if (@delegate != null)
			{
				((AssignmentDeleteEventHandler)@delegate)(this, e);
			}
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00057258 File Offset: 0x00055458
		protected virtual void OnNavigationCommand(NavigationCommandEventArgs e)
		{
			Delegate @delegate = base.Events[RadGantt.navigationCommandEvent];
			if (@delegate != null)
			{
				((NavigationCommandEventHandler)@delegate)(this, e);
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06001B25 RID: 6949 RVA: 0x00057286 File Offset: 0x00055486
		// (remove) Token: 0x06001B26 RID: 6950 RVA: 0x00057299 File Offset: 0x00055499
		[Category("Behavior")]
		[Description("Fires when an column is about to be created. ")]
		public event ColumnCreatingEventHandler ColumnCreating
		{
			add
			{
				base.Events.AddHandler(RadGantt.columnCreatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGantt.columnCreatingEvent, value);
			}
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06001B27 RID: 6951 RVA: 0x000572AC File Offset: 0x000554AC
		// (remove) Token: 0x06001B28 RID: 6952 RVA: 0x000572BF File Offset: 0x000554BF
		[Category("Behavior")]
		[Description("Fires when an column is about to be created. ")]
		public event ColumnCreatedEventHandler ColumnCreated
		{
			add
			{
				base.Events.AddHandler(RadGantt.columnCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGantt.columnCreatedEvent, value);
			}
		}

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06001B29 RID: 6953 RVA: 0x000572D2 File Offset: 0x000554D2
		// (remove) Token: 0x06001B2A RID: 6954 RVA: 0x000572E5 File Offset: 0x000554E5
		[Description("Fires when a task's collection is about to be inserted in the database.")]
		[Category("Behavior")]
		public event TaskInsertEventHandler TaskInsert
		{
			add
			{
				base.Events.AddHandler(RadGantt.taskInsertEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGantt.taskInsertEvent, value);
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06001B2B RID: 6955 RVA: 0x000572F8 File Offset: 0x000554F8
		// (remove) Token: 0x06001B2C RID: 6956 RVA: 0x0005730B File Offset: 0x0005550B
		[Category("Behavior")]
		[Description("Fires when a task's collection is about to be updated through the provider.")]
		public event TaskUpdateEventHandler TaskUpdate
		{
			add
			{
				base.Events.AddHandler(RadGantt.taskUpdateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGantt.taskUpdateEvent, value);
			}
		}

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06001B2D RID: 6957 RVA: 0x0005731E File Offset: 0x0005551E
		// (remove) Token: 0x06001B2E RID: 6958 RVA: 0x00057331 File Offset: 0x00055531
		[Category("Behavior")]
		[Description("Fires when a task's collection is about to be deleted from the database through the provider.")]
		public event TaskDeleteEventHandler TaskDelete
		{
			add
			{
				base.Events.AddHandler(RadGantt.taskDeleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGantt.taskDeleteEvent, value);
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06001B2F RID: 6959 RVA: 0x00057344 File Offset: 0x00055544
		// (remove) Token: 0x06001B30 RID: 6960 RVA: 0x00057357 File Offset: 0x00055557
		[Category("Behavior")]
		[Description("Fires when a dependency's collection is about to be inserted in the database.")]
		public event DependencyInsertEventHandler DependencyInsert
		{
			add
			{
				base.Events.AddHandler(RadGantt.dependencyInsertEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGantt.dependencyInsertEvent, value);
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06001B31 RID: 6961 RVA: 0x0005736A File Offset: 0x0005556A
		// (remove) Token: 0x06001B32 RID: 6962 RVA: 0x0005737D File Offset: 0x0005557D
		[Description("Fires when a dependency's collection is about to be deleted from the database through the provider.")]
		[Category("Behavior")]
		public event DependencyDeleteEventHandler DependencyDelete
		{
			add
			{
				base.Events.AddHandler(RadGantt.dependencyDeleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGantt.dependencyDeleteEvent, value);
			}
		}

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06001B33 RID: 6963 RVA: 0x00057390 File Offset: 0x00055590
		// (remove) Token: 0x06001B34 RID: 6964 RVA: 0x000573A3 File Offset: 0x000555A3
		[Category("Behavior")]
		[Description("Fires when an assignment's collection is about to be inserted in the database.")]
		public event AssignmentInsertEventHandler AssignmentInsert
		{
			add
			{
				base.Events.AddHandler(RadGantt.assignmentInsertEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGantt.assignmentInsertEvent, value);
			}
		}

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06001B35 RID: 6965 RVA: 0x000573B6 File Offset: 0x000555B6
		// (remove) Token: 0x06001B36 RID: 6966 RVA: 0x000573C9 File Offset: 0x000555C9
		[Category("Behavior")]
		[Description("Fires when an assignment's collection is about to be updated through the provider.")]
		public event AssignmentUpdateEventHandler AssignmentUpdate
		{
			add
			{
				base.Events.AddHandler(RadGantt.assignmentUpdateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGantt.assignmentUpdateEvent, value);
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06001B37 RID: 6967 RVA: 0x000573DC File Offset: 0x000555DC
		// (remove) Token: 0x06001B38 RID: 6968 RVA: 0x000573EF File Offset: 0x000555EF
		[Description("Fires when an assignment's collection is about to be deleted from the database through the provider.")]
		[Category("Behavior")]
		public event AssignmentDeleteEventHandler AssignmentDelete
		{
			add
			{
				base.Events.AddHandler(RadGantt.assignmentDeleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGantt.assignmentDeleteEvent, value);
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06001B39 RID: 6969 RVA: 0x00057402 File Offset: 0x00055602
		// (remove) Token: 0x06001B3A RID: 6970 RVA: 0x00057415 File Offset: 0x00055615
		[Description("Fires when the RadGantt executes a view change command.")]
		[Category("Behavior")]
		public event NavigationCommandEventHandler NavigationCommand
		{
			add
			{
				base.Events.AddHandler(RadGantt.navigationCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadGantt.navigationCommandEvent, value);
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x00057428 File Offset: 0x00055628
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x00057457 File Offset: 0x00055657
		[DefaultValue("")]
		[ClientPropertyName("taskResizeStart")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientTaskResizeStart
		{
			get
			{
				if (this.ViewState["OnClientTaskResizeStart"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientTaskResizeStart"];
			}
			set
			{
				this.ViewState["OnClientTaskResizeStart"] = value;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0005746A File Offset: 0x0005566A
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x00057499 File Offset: 0x00055699
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("taskResizeEnd")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		public string OnClientTaskResizeEnd
		{
			get
			{
				if (this.ViewState["OnClientTaskResizeEnd"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientTaskResizeEnd"];
			}
			set
			{
				this.ViewState["OnClientTaskResizeEnd"] = value;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x000574AC File Offset: 0x000556AC
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x000574DB File Offset: 0x000556DB
		[DefaultValue("")]
		[ClientPropertyName("columnResized")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientColumnResized
		{
			get
			{
				if (this.ViewState["OnClientColumnResized"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientColumnResized"];
			}
			set
			{
				this.ViewState["OnClientColumnResized"] = value;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x000574EE File Offset: 0x000556EE
		// (set) Token: 0x06001B42 RID: 6978 RVA: 0x0005751D File Offset: 0x0005571D
		[ClientPropertyName("taskResizing")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientTaskResizing
		{
			get
			{
				if (this.ViewState["OnClientTaskResizing"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientTaskResizing"];
			}
			set
			{
				this.ViewState["OnClientTaskResizing"] = value;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x00057530 File Offset: 0x00055730
		// (set) Token: 0x06001B44 RID: 6980 RVA: 0x00057550 File Offset: 0x00055750
		[Category("Client-side events")]
		[ClientControlEvent]
		[Description("The name of the JavaScript function called when a task is about to be moved.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("taskMoveStart")]
		[DefaultValue("")]
		public string OnClientTaskMoveStart
		{
			get
			{
				return (string)(this.ViewState["OnClientTaskMoveStart"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTaskMoveStart"] = value;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x00057563 File Offset: 0x00055763
		// (set) Token: 0x06001B46 RID: 6982 RVA: 0x00057583 File Offset: 0x00055783
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when a task is being moved.")]
		[ClientControlEvent]
		[ClientPropertyName("taskMoving")]
		[DefaultValue("")]
		public string OnClientTaskMoving
		{
			get
			{
				return (string)(this.ViewState["OnClientTaskMoving"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTaskMoving"] = value;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x00057596 File Offset: 0x00055796
		// (set) Token: 0x06001B48 RID: 6984 RVA: 0x000575B6 File Offset: 0x000557B6
		[DefaultValue("")]
		[ClientPropertyName("taskMoveEnd")]
		[Description("The name of the JavaScript function called when a task has been moved.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientTaskMoveEnd
		{
			get
			{
				return (string)(this.ViewState["OnClientTaskMoveEnd"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTaskMoveEnd"] = value;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x000575C9 File Offset: 0x000557C9
		// (set) Token: 0x06001B4A RID: 6986 RVA: 0x000575E9 File Offset: 0x000557E9
		[DefaultValue("")]
		[ClientPropertyName("navigationCommand")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when the scheduler is about to execute a navigation command.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientNavigationCommand
		{
			get
			{
				return (string)(this.ViewState["OnClientNavigationCommand"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNavigationCommand"] = value;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x000575FC File Offset: 0x000557FC
		// (set) Token: 0x06001B4C RID: 6988 RVA: 0x0005761C File Offset: 0x0005581C
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("pdfExporting")]
		public string OnClientPdfExporting
		{
			get
			{
				return (string)(this.ViewState["OnClientPdfExporting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientPdfExporting"] = value;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0005762F File Offset: 0x0005582F
		// (set) Token: 0x06001B4E RID: 6990 RVA: 0x0005764F File Offset: 0x0005584F
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[ClientPropertyName("dataBound")]
		[DefaultValue("")]
		public string OnClientDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDataBound"] = value;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x00057662 File Offset: 0x00055862
		// (set) Token: 0x06001B50 RID: 6992 RVA: 0x00057682 File Offset: 0x00055882
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("kendoWidgetInitializing")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnClientKendoWidgetInitializing
		{
			get
			{
				return (string)(this.ViewState["OnClientKendoWidgetInitializing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientKendoWidgetInitializing"] = value;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00057695 File Offset: 0x00055895
		// (set) Token: 0x06001B52 RID: 6994 RVA: 0x000576B5 File Offset: 0x000558B5
		[ClientControlEvent]
		[ClientPropertyName("requestStart")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientRequestStart
		{
			get
			{
				return (string)(this.ViewState["OnClientRequestStart"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRequestStart"] = value;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x000576C8 File Offset: 0x000558C8
		// (set) Token: 0x06001B54 RID: 6996 RVA: 0x000576E8 File Offset: 0x000558E8
		[ClientControlEvent]
		[ClientPropertyName("requestEnd")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public string OnClientRequestEnd
		{
			get
			{
				return (string)(this.ViewState["OnClientRequestEnd"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRequestEnd"] = value;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x000576FB File Offset: 0x000558FB
		// (set) Token: 0x06001B56 RID: 6998 RVA: 0x0005771B File Offset: 0x0005591B
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("requestFailed")]
		[Category("Client-side events")]
		public string OnClientRequestFailed
		{
			get
			{
				return (string)(this.ViewState["OnClientRequestFailed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRequestFailed"] = value;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0005772E File Offset: 0x0005592E
		// (set) Token: 0x06001B58 RID: 7000 RVA: 0x0005774E File Offset: 0x0005594E
		[ClientPropertyName("togglePlannedTasks")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientTogglePlannedTasks
		{
			get
			{
				return (string)(this.ViewState["OnClientTogglePlannedTasks"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTogglePlannedTasks"] = value;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x00057761 File Offset: 0x00055961
		// (set) Token: 0x06001B5A RID: 7002 RVA: 0x00057781 File Offset: 0x00055981
		[DefaultValue("")]
		[ClientPropertyName("taskSelectionChanged")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientTaskSelectionChanged
		{
			get
			{
				return (string)(this.ViewState["OnClientTaskSelectionChanged"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTaskSelectionChanged"] = value;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x00057794 File Offset: 0x00055994
		// (set) Token: 0x06001B5C RID: 7004 RVA: 0x000577B4 File Offset: 0x000559B4
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("taskEditing")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientTaskEditing
		{
			get
			{
				return (string)(this.ViewState["OnClientTaskEditing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTaskEditing"] = value;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x000577C7 File Offset: 0x000559C7
		// (set) Token: 0x06001B5E RID: 7006 RVA: 0x000577E7 File Offset: 0x000559E7
		[ClientPropertyName("inserting")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientInserting
		{
			get
			{
				return (string)(this.ViewState["OnClientInserting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientInserting"] = value;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x000577FA File Offset: 0x000559FA
		// (set) Token: 0x06001B60 RID: 7008 RVA: 0x0005781A File Offset: 0x00055A1A
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientPropertyName("removing")]
		public string OnClientRemoving
		{
			get
			{
				return (string)(this.ViewState["OnClientRemoving"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRemoving"] = value;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0005782D File Offset: 0x00055A2D
		// (set) Token: 0x06001B62 RID: 7010 RVA: 0x0005784D File Offset: 0x00055A4D
		[ClientControlEvent]
		[ClientPropertyName("taskSaving")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientTaskSaving
		{
			get
			{
				return (string)(this.ViewState["OnClientTaskSaving"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTaskSaving"] = value;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x00057860 File Offset: 0x00055A60
		// (set) Token: 0x06001B64 RID: 7012 RVA: 0x00057880 File Offset: 0x00055A80
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("taskSaving")]
		public string OnClientTaskUpdateCancel
		{
			get
			{
				return (string)(this.ViewState["OnClientTaskUpdateCancel"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTaskUpdateCancel"] = value;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x00057893 File Offset: 0x00055A93
		// (set) Token: 0x06001B66 RID: 7014 RVA: 0x000578B3 File Offset: 0x00055AB3
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The JavaScript function executed when RadGantt is initialized")]
		[ClientPropertyName("load")]
		[Category("Client-side events")]
		[ClientControlEvent]
		public string OnClientLoad
		{
			get
			{
				return (string)(this.ViewState["OnClientLoad"] ?? "");
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x000578C6 File Offset: 0x00055AC6
		// (set) Token: 0x06001B68 RID: 7016 RVA: 0x000578E7 File Offset: 0x00055AE7
		[DefaultValue(true)]
		[Description("Value indicating whether sorting is enabled for the tree list part.")]
		[Category("Behavior")]
		public bool AllowSorting
		{
			get
			{
				return (bool)(this.ViewState["AllowSorting"] ?? true);
			}
			set
			{
				this.ViewState["AllowSorting"] = value;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x000578FF File Offset: 0x00055AFF
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x0005791B File Offset: 0x00055B1B
		[Description("Gets or sets the date to which the timeline of the currently selected view is scrolled.")]
		[ClientPropertyName("selectedDate")]
		[DefaultValue(null)]
		[ClientControlProperty]
		[Category("Behavior")]
		public DateTime? SelectedDate
		{
			get
			{
				return (DateTime?)(this.ViewState["SelectedDate"] ?? null);
			}
			set
			{
				this.ViewState["SelectedDate"] = value;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x00057933 File Offset: 0x00055B33
		// (set) Token: 0x06001B6C RID: 7020 RVA: 0x0005794F File Offset: 0x00055B4F
		[DefaultValue(null)]
		[ClientPropertyName("rangeStart")]
		[Category("Behavior")]
		[Description("Gets or sets the start range of the currently selected view.")]
		[ClientControlProperty]
		public DateTime? RangeStart
		{
			get
			{
				return (DateTime?)(this.ViewState["RangeStart"] ?? null);
			}
			set
			{
				this.ViewState["RangeStart"] = value;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x00057967 File Offset: 0x00055B67
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x00057983 File Offset: 0x00055B83
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(null)]
		[ClientPropertyName("rangeEnd")]
		[Description("Gets or sets the end range of the currently selected view.")]
		public DateTime? RangeEnd
		{
			get
			{
				return (DateTime?)(this.ViewState["RangeEnd"] ?? null);
			}
			set
			{
				this.ViewState["RangeEnd"] = value;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x0005799B File Offset: 0x00055B9B
		// (set) Token: 0x06001B70 RID: 7024 RVA: 0x000579BC File Offset: 0x00055BBC
		[ClientControlProperty]
		[ClientPropertyName("allowTaskInsert")]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the user is able to insert a task.")]
		[DefaultValue(true)]
		public bool AllowTaskInsert
		{
			get
			{
				return (bool)(this.ViewState["AllowTaskInsert"] ?? true);
			}
			set
			{
				this.ViewState["AllowTaskInsert"] = value;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x000579D4 File Offset: 0x00055BD4
		// (set) Token: 0x06001B72 RID: 7026 RVA: 0x000579F5 File Offset: 0x00055BF5
		[ClientControlProperty]
		[ClientPropertyName("allowPlannedTasks")]
		[Description("Gets or sets a value indicating whether the user is able to insert a task.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool AllowPlannedTasks
		{
			get
			{
				return (bool)(this.ViewState["AllowPlannedTasks"] ?? false);
			}
			set
			{
				this.ViewState["AllowPlannedTasks"] = value;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x00057A0D File Offset: 0x00055C0D
		// (set) Token: 0x06001B74 RID: 7028 RVA: 0x00057A2E File Offset: 0x00055C2E
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("If set to true, the Gantt Timeline will render both the planned and the actual execution of tasks")]
		[ClientPropertyName("showPlannedTasks")]
		public bool ShowPlannedTasks
		{
			get
			{
				return (bool)(this.ViewState["ShowPlannedTasks"] ?? false);
			}
			set
			{
				this.ViewState["ShowPlannedTasks"] = value;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x00057A46 File Offset: 0x00055C46
		// (set) Token: 0x06001B76 RID: 7030 RVA: 0x00057A67 File Offset: 0x00055C67
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the user is able to update a task.")]
		[ClientControlProperty]
		[ClientPropertyName("allowTaskUpdate")]
		[DefaultValue(true)]
		public bool AllowTaskUpdate
		{
			get
			{
				return (bool)(this.ViewState["AllowTaskUpdate"] ?? true);
			}
			set
			{
				this.ViewState["AllowTaskUpdate"] = value;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x00057A7F File Offset: 0x00055C7F
		// (set) Token: 0x06001B78 RID: 7032 RVA: 0x00057AA0 File Offset: 0x00055CA0
		[ClientPropertyName("allowTaskDelete")]
		[Category("Behavior")]
		[DefaultValue(true)]
		[ClientControlProperty]
		[Description("Gets or sets a value indicating whether the user is able to delete a task.")]
		public bool AllowTaskDelete
		{
			get
			{
				return (bool)(this.ViewState["AllowTaskDelete"] ?? true);
			}
			set
			{
				this.ViewState["AllowTaskDelete"] = value;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x00057AB8 File Offset: 0x00055CB8
		// (set) Token: 0x06001B7A RID: 7034 RVA: 0x00057AD9 File Offset: 0x00055CD9
		[DefaultValue(true)]
		[ClientPropertyName("allowTaskMove")]
		[Description("Gets or sets a value indicating whether the user is able to move a task.")]
		[ClientControlProperty]
		[Category("Behavior")]
		public bool AllowTaskMove
		{
			get
			{
				return (bool)(this.ViewState["AllowTaskMove"] ?? true);
			}
			set
			{
				this.ViewState["AllowTaskMove"] = value;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x00057AF1 File Offset: 0x00055CF1
		// (set) Token: 0x06001B7C RID: 7036 RVA: 0x00057B12 File Offset: 0x00055D12
		[ClientPropertyName("allowTaskResize")]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the user is able to resize a task.")]
		[DefaultValue(true)]
		[ClientControlProperty]
		public bool AllowTaskResize
		{
			get
			{
				return (bool)(this.ViewState["AllowTaskResize"] ?? true);
			}
			set
			{
				this.ViewState["AllowTaskResize"] = value;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x00057B2A File Offset: 0x00055D2A
		// (set) Token: 0x06001B7E RID: 7038 RVA: 0x00057B4B File Offset: 0x00055D4B
		[ClientControlProperty]
		[Description("Gets or sets a value indicating whether the user is able to reorder a task.")]
		[ClientPropertyName("allowTaskReorder")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool AllowTaskReorder
		{
			get
			{
				return (bool)(this.ViewState["AllowTaskReorder"] ?? true);
			}
			set
			{
				this.ViewState["AllowTaskReorder"] = value;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x00057B63 File Offset: 0x00055D63
		// (set) Token: 0x06001B80 RID: 7040 RVA: 0x00057B84 File Offset: 0x00055D84
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the user is able to drag PercetComplete of a task.")]
		[DefaultValue(true)]
		[ClientControlProperty]
		[ClientPropertyName("allowPercentCompleteDrag")]
		public bool AllowPercentCompleteDrag
		{
			get
			{
				return (bool)(this.ViewState["AllowPercentCompleteDrag"] ?? true);
			}
			set
			{
				this.ViewState["AllowPercentCompleteDrag"] = value;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x00057B9C File Offset: 0x00055D9C
		// (set) Token: 0x06001B82 RID: 7042 RVA: 0x00057BBD File Offset: 0x00055DBD
		[Category("Behavior")]
		[ClientPropertyName("allowDependencyInsert")]
		[Description("Gets or sets a value indicating whether the user is able to insert a dependency.")]
		[DefaultValue(true)]
		[ClientControlProperty]
		public bool AllowDependencyInsert
		{
			get
			{
				return (bool)(this.ViewState["AllowDependencyInsert"] ?? true);
			}
			set
			{
				this.ViewState["AllowDependencyInsert"] = value;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x00057BD5 File Offset: 0x00055DD5
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x00057BF6 File Offset: 0x00055DF6
		[Description("Gets or sets a value indicating whether the user is able to delete a dependency.")]
		[ClientControlProperty]
		[ClientPropertyName("allowDependencyDelete")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool AllowDependencyDelete
		{
			get
			{
				return (bool)(this.ViewState["AllowDependencyDelete"] ?? true);
			}
			set
			{
				this.ViewState["AllowDependencyDelete"] = value;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x00057C0E File Offset: 0x00055E0E
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x00057C2F File Offset: 0x00055E2F
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientPropertyName("enableResources")]
		[Description("Value indicating whether the resources functionality is enabled.")]
		[ClientControlProperty]
		public bool EnableResources
		{
			get
			{
				return (bool)(this.ViewState["EnableResources"] ?? false);
			}
			set
			{
				this.ViewState["EnableResources"] = value;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x00057C47 File Offset: 0x00055E47
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x00057C68 File Offset: 0x00055E68
		[Description("Value indicating whether the resources functionality is enabled.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[ClientPropertyName("allowColumnResize")]
		public bool AllowColumnResize
		{
			get
			{
				return (bool)(this.ViewState["AllowColumnResize"] ?? false);
			}
			set
			{
				this.ViewState["AllowColumnResize"] = value;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x00057C80 File Offset: 0x00055E80
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x00057CA0 File Offset: 0x00055EA0
		[Description("Gets or sets the HTML template of the RadGantt task.")]
		[ClientControlProperty]
		[ClientPropertyName("clientTemplate")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue("")]
		public string ClientTemplate
		{
			get
			{
				return (this.ViewState["ClientTemplate"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientTemplate"] = value;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00057CB3 File Offset: 0x00055EB3
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x00057CD4 File Offset: 0x00055ED4
		[ClientControlProperty]
		[Category("Behavior")]
		[ClientPropertyName("enablePdfExport")]
		[Description("Value indicating whether the export to PDF functionality is enabled.")]
		[DefaultValue(false)]
		public bool EnablePdfExport
		{
			get
			{
				return (bool)(this.ViewState["EnablePdfExport"] ?? false);
			}
			set
			{
				this.ViewState["EnablePdfExport"] = value;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x00057CEC File Offset: 0x00055EEC
		// (set) Token: 0x06001B8E RID: 7054 RVA: 0x00057D1A File Offset: 0x00055F1A
		[Category("Appearance")]
		[Description("RadGantt width")]
		[ClientPropertyName("width")]
		[DefaultValue(typeof(Unit), "100%")]
		[ClientControlProperty]
		public override Unit Width
		{
			get
			{
				return (Unit)(this.ViewState["Width"] ?? Unit.Percentage(100.0));
			}
			set
			{
				this.ViewState["Width"] = value;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x00057D32 File Offset: 0x00055F32
		// (set) Token: 0x06001B90 RID: 7056 RVA: 0x00057D60 File Offset: 0x00055F60
		[Description("RadGantt list width")]
		[Category("Appearance")]
		[ClientControlProperty]
		[ClientPropertyName("listWidth")]
		[DefaultValue(typeof(Unit), "30%")]
		public virtual Unit ListWidth
		{
			get
			{
				return (Unit)(this.ViewState["ListWidth"] ?? Unit.Percentage(30.0));
			}
			set
			{
				this.ViewState["ListWidth"] = value;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x00057D78 File Offset: 0x00055F78
		// (set) Token: 0x06001B92 RID: 7058 RVA: 0x00057D9D File Offset: 0x00055F9D
		[ClientPropertyName("rowHeight")]
		[ClientControlProperty]
		[Category("Appearance")]
		[Description("The height of each RadGantt row")]
		[DefaultValue(typeof(Unit), "")]
		public Unit RowHeight
		{
			get
			{
				return (Unit)(this.ViewState["RowHeight"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["RowHeight"] = value;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x00057DB5 File Offset: 0x00055FB5
		// (set) Token: 0x06001B94 RID: 7060 RVA: 0x00057DDF File Offset: 0x00055FDF
		[Description("RadGantt height")]
		[DefaultValue(typeof(Unit), "700px")]
		[Category("Appearance")]
		[ClientControlProperty]
		[ClientPropertyName("height")]
		public override Unit Height
		{
			get
			{
				return (Unit)(this.ViewState["Height"] ?? Unit.Pixel(700));
			}
			set
			{
				this.ViewState["Height"] = value;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x00057DF7 File Offset: 0x00055FF7
		// (set) Token: 0x06001B96 RID: 7062 RVA: 0x00057E2B File Offset: 0x0005602B
		[DefaultValue("Integrated")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("The name of the custom provider to use, as configured in web.config.")]
		[Category("Data")]
		public string ProviderName
		{
			get
			{
				if (!base.DesignMode)
				{
					return this.Provider.Name;
				}
				return (string)(this.ViewState["ProviderName"] ?? "Integrated");
			}
			set
			{
				if (!base.DesignMode)
				{
					this.Provider = GanttProviderFactory.GetProvider(this, value);
					return;
				}
				this.ViewState["ProviderName"] = ((value == string.Empty) ? null : value);
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x00057E64 File Offset: 0x00056064
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x00057E6C File Offset: 0x0005606C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public GanttProviderBase Provider
		{
			get
			{
				return this._tasksProvider;
			}
			set
			{
				this._tasksProvider = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x00057E7B File Offset: 0x0005607B
		// (set) Token: 0x06001B9A RID: 7066 RVA: 0x00057E9C File Offset: 0x0005609C
		[Description("The selected view type")]
		[Category("Layout")]
		[DefaultValue(GanttViewType.DayView)]
		public GanttViewType SelectedView
		{
			get
			{
				return (GanttViewType)(this.ViewState["SelectedView"] ?? GanttViewType.DayView);
			}
			set
			{
				this.ViewState["SelectedView"] = value;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x00057EB4 File Offset: 0x000560B4
		// (set) Token: 0x06001B9C RID: 7068 RVA: 0x00057ED4 File Offset: 0x000560D4
		[Description("Gets or sets the DependenciesDataSource used for data binding.")]
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string DependenciesDataSourceID
		{
			get
			{
				return ((string)this.ViewState["DependenciesDataSourceID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DependenciesDataSourceID"] = value;
				this._depBinderIDChanged = true;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x00057EF4 File Offset: 0x000560F4
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x00057F01 File Offset: 0x00056101
		[Browsable(false)]
		[Description("Data source for dependencies")]
		[Category("Data")]
		public object DependenciesDataSource
		{
			get
			{
				return this.DependenciesBinder.DataSource;
			}
			set
			{
				this.DependenciesBinder.DataSource = value;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x00057F0F File Offset: 0x0005610F
		// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x00057F2F File Offset: 0x0005612F
		[DefaultValue("")]
		[Description("Gets or sets the ResourcesDataSourceID used for data binding.")]
		[Category("Data")]
		[NotifyParentProperty(true)]
		public virtual string ResourcesDataSourceID
		{
			get
			{
				return ((string)this.ViewState["ResourcesDataSourceID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ResourcesDataSourceID"] = value;
				this._resBinderIDChanged = true;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x00057F4F File Offset: 0x0005614F
		// (set) Token: 0x06001BA2 RID: 7074 RVA: 0x00057F5C File Offset: 0x0005615C
		[Description("Data source for resources")]
		[Browsable(false)]
		[Category("Data")]
		public object ResourcesDataSource
		{
			get
			{
				return this.ResourcesBinder.DataSource;
			}
			set
			{
				this.ResourcesBinder.DataSource = value;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x00057F6A File Offset: 0x0005616A
		// (set) Token: 0x06001BA4 RID: 7076 RVA: 0x00057F8A File Offset: 0x0005618A
		[Description("Gets or sets the AssignmentsDataSourceID used for data binding.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Data")]
		public virtual string AssignmentsDataSourceID
		{
			get
			{
				return ((string)this.ViewState["AssignmentsDataSourceID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["AssignmentsDataSourceID"] = value;
				this._asmBinderIDChanged = true;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x00057FAA File Offset: 0x000561AA
		// (set) Token: 0x06001BA6 RID: 7078 RVA: 0x00057FB7 File Offset: 0x000561B7
		[Description("Data source for resource assignments")]
		[Category("Data")]
		[Browsable(false)]
		public object AssignmentsDataSource
		{
			get
			{
				return this.AssignmentsBinder.DataSource;
			}
			set
			{
				this.AssignmentsBinder.DataSource = value;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x00057FC5 File Offset: 0x000561C5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual GanttDataBindings DataBindings
		{
			get
			{
				if (this._bindings == null)
				{
					this._bindings = GanttDataBindings.Empty;
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._bindings).TrackViewState();
					}
				}
				return this._bindings;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x00057FF3 File Offset: 0x000561F3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual TaskCollection Tasks
		{
			get
			{
				if (this._tasksCollection == null)
				{
					this._tasksCollection = new TaskCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._tasksCollection).TrackViewState();
					}
				}
				return this._tasksCollection;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x00058022 File Offset: 0x00056222
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual DependencyCollection Dependencies
		{
			get
			{
				if (this._dependenciesCollection == null)
				{
					this._dependenciesCollection = new DependencyCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._dependenciesCollection).TrackViewState();
					}
				}
				return this._dependenciesCollection;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x00058051 File Offset: 0x00056251
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual ResourceCollection Resources
		{
			get
			{
				if (this._resourcesCollection == null)
				{
					this._resourcesCollection = new ResourceCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._resourcesCollection).TrackViewState();
					}
				}
				return this._resourcesCollection;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x00058080 File Offset: 0x00056280
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual AssignmentCollection Assignments
		{
			get
			{
				if (this._assignmentsCollection == null)
				{
					this._assignmentsCollection = new AssignmentCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._assignmentsCollection).TrackViewState();
					}
				}
				return this._assignmentsCollection;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x000580AF File Offset: 0x000562AF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ColumnCollection Columns
		{
			get
			{
				if (this._columnsCollection == null)
				{
					this._columnsCollection = new ColumnCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._columnsCollection).TrackViewState();
					}
					this.EnsureChildControls();
				}
				return this._columnsCollection;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x000580E4 File Offset: 0x000562E4
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual CustomFieldCollection CustomTaskFields
		{
			get
			{
				if (this._customTaskFields == null)
				{
					this._customTaskFields = new CustomFieldCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._customTaskFields).TrackViewState();
					}
				}
				return this._customTaskFields;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x00058113 File Offset: 0x00056313
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual FilterEntryCollection Entries
		{
			get
			{
				if (this._entries == null)
				{
					this._entries = new FilterEntryCollection();
				}
				return this._entries;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0005812E File Offset: 0x0005632E
		[Description("Day view settings")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Layout")]
		public DayViewSettings DayView
		{
			get
			{
				return this._dayViewSettings;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x00058136 File Offset: 0x00056336
		[Description("Week view settings")]
		[Category("Layout")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public WeekViewSettings WeekView
		{
			get
			{
				return this._weekViewSettings;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x0005813E File Offset: 0x0005633E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Layout")]
		[Description("Month view settings")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MonthViewSettings MonthView
		{
			get
			{
				return this._monthViewSettings;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x00058146 File Offset: 0x00056346
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Layout")]
		[Description("Year view settings")]
		public YearViewSettings YearView
		{
			get
			{
				return this._yearViewSettings;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0005814E File Offset: 0x0005634E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Export")]
		[Description("Export settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GanttExportSettings ExportSettings
		{
			get
			{
				if (this._exportSettings == null)
				{
					this._exportSettings = new GanttExportSettings(this.ViewState);
				}
				return this._exportSettings;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x0005816F File Offset: 0x0005636F
		// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x00058190 File Offset: 0x00056390
		[DefaultValue(DayOfWeek.Monday)]
		[Category("Appearance")]
		[Description("The first day of the work week")]
		[ClientControlProperty]
		public DayOfWeek WorkWeekStart
		{
			get
			{
				return (DayOfWeek)(this.ViewState["WorkWeekStart"] ?? DayOfWeek.Monday);
			}
			set
			{
				this.ViewState["WorkWeekStart"] = value;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x000581A8 File Offset: 0x000563A8
		// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x000581C9 File Offset: 0x000563C9
		[ClientControlProperty]
		[DefaultValue(DayOfWeek.Friday)]
		[Description("The last day of the work week.")]
		[Category("Appearance")]
		public DayOfWeek WorkWeekEnd
		{
			get
			{
				return (DayOfWeek)(this.ViewState["WorkWeekEnd"] ?? DayOfWeek.Friday);
			}
			set
			{
				this.ViewState["WorkWeekEnd"] = value;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x000581E1 File Offset: 0x000563E1
		// (set) Token: 0x06001BB9 RID: 7097 RVA: 0x00058202 File Offset: 0x00056402
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Whether to start in full time mode")]
		[ClientControlProperty]
		public bool ShowFullTime
		{
			get
			{
				return (bool)(this.ViewState["ShowFullTime"] ?? false);
			}
			set
			{
				this.ViewState["ShowFullTime"] = value;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x0005821A File Offset: 0x0005641A
		// (set) Token: 0x06001BBB RID: 7099 RVA: 0x0005823B File Offset: 0x0005643B
		[Description("Whether to start in full week mode")]
		[ClientControlProperty]
		[Category("Appearance")]
		[DefaultValue(true)]
		public bool ShowFullWeek
		{
			get
			{
				return (bool)(this.ViewState["ShowFullWeek"] ?? true);
			}
			set
			{
				this.ViewState["ShowFullWeek"] = value;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x00058253 File Offset: 0x00056453
		// (set) Token: 0x06001BBD RID: 7101 RVA: 0x00058260 File Offset: 0x00056460
		[Obsolete("This property is obsolete in favor of the TasksTooltipSettings.Visible property.")]
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Whether to show the task tooltip")]
		public bool ShowTooltip
		{
			get
			{
				return this.TasksTooltipSettings.Visible;
			}
			set
			{
				this.TasksTooltipSettings.Visible = value;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0005826E File Offset: 0x0005646E
		[Description("Default Tooltip options for this the Gantt Tasks.")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ClientPropertyName("tooltip")]
		[Browsable(false)]
		public TasksTooltip TasksTooltipSettings
		{
			get
			{
				if (this._tasksTooltip == null)
				{
					this._tasksTooltip = new TasksTooltip();
				}
				return this._tasksTooltip;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x00058289 File Offset: 0x00056489
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[ClientPropertyName("toolbar")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Default Tooltip options for this the Gantt Tasks.")]
		[Browsable(false)]
		[ClientControlProperty]
		[DefaultValue(null)]
		public GanttToolbar Toolbar
		{
			get
			{
				if (this._toolbar == null)
				{
					this._toolbar = new GanttToolbar();
				}
				return this._toolbar;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x000582A4 File Offset: 0x000564A4
		// (set) Token: 0x06001BC1 RID: 7105 RVA: 0x000582C5 File Offset: 0x000564C5
		[Category("Behavior")]
		[ClientPropertyName("showCurrentTimeMarker")]
		[Description("Value indicating whether the current time marker is visible.")]
		[DefaultValue(true)]
		[ClientControlProperty]
		public bool ShowCurrentTimeMarker
		{
			get
			{
				return (bool)(this.ViewState["ShowCurrentTimeMarker"] ?? true);
			}
			set
			{
				this.ViewState["ShowCurrentTimeMarker"] = value;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x000582DD File Offset: 0x000564DD
		// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x00058302 File Offset: 0x00056502
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(10000)]
		[ClientPropertyName("currentTimeMarkerInterval")]
		[Description("Value indicating the number of milliseconds after which the current time marker is updated.")]
		public int CurrentTimeMarkerInterval
		{
			get
			{
				return (int)(this.ViewState["CurrentTimeMarkerInterval"] ?? 10000);
			}
			set
			{
				this.ViewState["CurrentTimeMarkerInterval"] = value;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x0005831A File Offset: 0x0005651A
		// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x0005833B File Offset: 0x0005653B
		[ClientPropertyName("displayDeleteConfirmation")]
		[Description("Whether to display the delete confirmation dialog.")]
		[Category("Behavior")]
		[DefaultValue(true)]
		[ClientControlProperty]
		public bool DisplayDeleteConfirmation
		{
			get
			{
				return (bool)(this.ViewState["DisplayDeleteConfirmation"] ?? true);
			}
			set
			{
				this.ViewState["DisplayDeleteConfirmation"] = value;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00058353 File Offset: 0x00056553
		// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x00058374 File Offset: 0x00056574
		[Category("Behavior")]
		[Description("Makes the control read-only.")]
		[ClientControlProperty]
		[ClientPropertyName("readOnly")]
		[DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get
			{
				return (bool)(this.ViewState["ReadOnly"] ?? false);
			}
			set
			{
				this.ViewState["ReadOnly"] = value;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x0005838C File Offset: 0x0005658C
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x000583AD File Offset: 0x000565AD
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Auto generates columns for the following fields:ID, Title, Start, End and PercentageComplete")]
		public virtual bool AutoGenerateColumns
		{
			get
			{
				return (bool)(this.ViewState["AutoGenerateColumns"] ?? true);
			}
			set
			{
				this.ViewState["AutoGenerateColumns"] = value;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x000583C5 File Offset: 0x000565C5
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x000583E5 File Offset: 0x000565E5
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		[Category("Misc")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x000583F8 File Offset: 0x000565F8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GanttStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new GanttStrings(new LocalizationProvider("RadGantt", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x00058437 File Offset: 0x00056637
		// (set) Token: 0x06001BCE RID: 7118 RVA: 0x00058458 File Offset: 0x00056658
		[Description("Value indicating where RadGantt will look for its .resx localization files.")]
		[Category("Misc")]
		[DefaultValue("")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x000584AB File Offset: 0x000566AB
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The web service settings to be used for binding this instance of RadGantt.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public WebServiceSettings WebServiceSettings
		{
			get
			{
				if (this._webServiceSettings == null)
				{
					this._webServiceSettings = new WebServiceSettings(this.ViewState);
				}
				return this._webServiceSettings;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x000584CC File Offset: 0x000566CC
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x000584ED File Offset: 0x000566ED
		[ClientPropertyName("snapToGrid")]
		[ClientControlProperty]
		[Description("Value that determines whether the tasks will snap to the nearest grid cell in the timeline.")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool SnapToGrid
		{
			get
			{
				return (bool)(this.ViewState["SnapToGrid"] ?? true);
			}
			set
			{
				this.ViewState["SnapToGrid"] = value;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00058505 File Offset: 0x00056705
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Description("Navigation settings")]
		[DefaultValue(null)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GanttKeyboardNavigationSettings KeyboardNavigationSettings
		{
			get
			{
				if (this._keyboardNavigationSettings == null)
				{
					this._keyboardNavigationSettings = new GanttKeyboardNavigationSettings(this.ViewState);
				}
				return this._keyboardNavigationSettings;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x00058526 File Offset: 0x00056726
		public override RenderMode ResolvedRenderMode
		{
			get
			{
				return RenderMode.Lightweight;
			}
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00058529 File Offset: 0x00056729
		public IList<ITask> GetAllTasks()
		{
			return this.Tasks.ToFlatList();
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00058538 File Offset: 0x00056738
		public void ImportXml(string xmlFileName)
		{
			XDocument xDocument = XDocument.Load(xmlFileName);
			this.ImportXml(xDocument);
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x00058553 File Offset: 0x00056753
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x0005855B File Offset: 0x0005675B
		[ClientControlProperty]
		[DefaultValue(0)]
		private int ScrollTop { get; set; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x00058564 File Offset: 0x00056764
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x0005856C File Offset: 0x0005676C
		[DefaultValue(0)]
		[ClientControlProperty]
		private int ScrollLeft { get; set; }

		// Token: 0x06001BDA RID: 7130 RVA: 0x00058575 File Offset: 0x00056775
		public RadGantt()
		{
			this.LoadProvider();
			this.CreateViews();
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x00058589 File Offset: 0x00056789
		protected virtual void LoadProvider()
		{
			this.ProviderName = "Integrated";
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x00058596 File Offset: 0x00056796
		protected virtual void CreateViews()
		{
			this._dayViewSettings = new DayViewSettings();
			this._weekViewSettings = new WeekViewSettings();
			this._monthViewSettings = new MonthViewSettings();
			this._yearViewSettings = new YearViewSettings();
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000585C4 File Offset: 0x000567C4
		protected virtual void CreateColumns()
		{
			if (this.AutoGenerateColumns)
			{
				this.Columns.Clear();
				foreach (ColumnMetaData columnMetaData in RadGantt.defaultColumns)
				{
					GanttBoundColumn ganttBoundColumn = this.DefaultColumnsFactory.CreateColumn(columnMetaData.Type);
					ganttBoundColumn.UniqueName = columnMetaData.DataField;
					ganttBoundColumn.DataField = columnMetaData.DataField;
					ganttBoundColumn.HeaderText = columnMetaData.Title;
					if (columnMetaData.DataField == "id")
					{
						ganttBoundColumn.Width = 50;
					}
					ganttBoundColumn.AllowSorting = this.AllowSorting;
					this.OnColumnCreating(new ColumnCreatingEventArgs(ganttBoundColumn));
					this.Columns.Add(ganttBoundColumn);
					this.OnColumnCreated(new ColumnCreatedEventArgs(ganttBoundColumn));
				}
			}
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000586B0 File Offset: 0x000568B0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			this.DescribeCustomFields(descriptor);
			this.DescribeTasks(descriptor);
			this.DescribeDependencies(descriptor);
			this.DescribeViews(descriptor);
			this.DescribeColumns(descriptor);
			this.DescribeLocalization(descriptor);
			descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			if (this.EnableResources)
			{
				this.DescribeResources(descriptor);
				this.DescribeAssignments(descriptor);
			}
			if (this.DayView.HourSpan != 1)
			{
				descriptor.AddProperty("hourSpan", this.DayView.HourSpan);
			}
			if (this.UsingWebServiceBinding)
			{
				this.WebServiceSettings.Describe("webServiceSettings", this.Serializer, descriptor);
			}
			if (this._keyboardNavigationSettings != null)
			{
				descriptor.AddScriptProperty("_navigationSettings", this.Serializer.Serialize(this.KeyboardNavigationSettings));
			}
			if (this._tasksTooltip != null && !this._tasksTooltip.IsDefault)
			{
				descriptor.AddScriptProperty("tooltip", this.Serializer.Serialize(this.TasksTooltipSettings));
			}
			if (this._toolbar != null)
			{
				string text = "javascript:";
				if (!string.IsNullOrEmpty(this.Toolbar.ClientTemplate))
				{
					new JavaScriptSerializerMarkers();
					if (this.Toolbar.ClientTemplate.TrimStart(new char[0]).StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
					{
						descriptor.AddScriptProperty("toolbar", this.Toolbar.ClientTemplate.TrimStart(new char[0]).Substring(text.Length).Trim());
					}
					else
					{
						descriptor.AddScriptProperty("toolbar", this.Serializer.Serialize(this.Toolbar.ClientTemplate));
					}
				}
				else if (this.Toolbar.Items.Count > 0)
				{
					descriptor.AddScriptProperty("toolbar", this.Serializer.Serialize(this.Toolbar.Items));
				}
			}
			if (this.EnablePdfExport)
			{
				descriptor.AddScriptProperty("pdfSettings", this.Serializer.Serialize(this.ExportSettings.Pdf));
			}
			this.DescribePostBack(descriptor);
			if (!string.Equals(base.RuntimeSkin, "Default", StringComparison.InvariantCulture))
			{
				descriptor.AddProperty("_skin", base.RuntimeSkin);
			}
			descriptor.AddProperty("_uniqueId", this.UniqueID);
			base.DescribeComponent(descriptor);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00058904 File Offset: 0x00056B04
		protected virtual void DescribePostBack(IScriptDescriptor descriptor)
		{
			if (base.Events[RadGantt.navigationCommandEvent] != null)
			{
				descriptor.AddProperty("_postBackOnNavigate", true);
			}
			if (base.Events[RadGantt.taskInsertEvent] != null)
			{
				descriptor.AddProperty("_postBackOnTaskInsert", true);
			}
			if (base.Events[RadGantt.taskUpdateEvent] != null)
			{
				descriptor.AddProperty("_postBackOnTaskUpdate", true);
			}
			if (base.Events[RadGantt.taskDeleteEvent] != null)
			{
				descriptor.AddProperty("_postBackOnTaskDelete", true);
			}
			if (base.Events[RadGantt.dependencyInsertEvent] != null)
			{
				descriptor.AddProperty("_postBackOnDependencyInsert", true);
			}
			if (base.Events[RadGantt.dependencyDeleteEvent] != null)
			{
				descriptor.AddProperty("_postBackOnDependencyDelete", true);
			}
			if (base.Events[RadGantt.assignmentInsertEvent] != null)
			{
				descriptor.AddProperty("_postBackOnAssignmentInsert", true);
			}
			if (base.Events[RadGantt.assignmentUpdateEvent] != null)
			{
				descriptor.AddProperty("_postBackOnAssignmentUpdate", true);
			}
			if (base.Events[RadGantt.assignmentDeleteEvent] != null)
			{
				descriptor.AddProperty("_postBackOnAssignmentDelete", true);
			}
			if (this.ShouldRenderPostBackReference)
			{
				descriptor.AddProperty("_postBackReference", this.Page.ClientScript.GetPostBackEventReference(this, "arguments"));
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x00058A78 File Offset: 0x00056C78
		internal bool ShouldRenderPostBackReference
		{
			get
			{
				return base.Events[RadGantt.navigationCommandEvent] != null || base.Events[RadGantt.taskInsertEvent] != null || base.Events[RadGantt.taskUpdateEvent] != null || base.Events[RadGantt.taskDeleteEvent] != null || base.Events[RadGantt.dependencyInsertEvent] != null || base.Events[RadGantt.dependencyDeleteEvent] != null || base.Events[RadGantt.assignmentInsertEvent] != null || base.Events[RadGantt.assignmentUpdateEvent] != null || base.Events[RadGantt.assignmentDeleteEvent] != null;
			}
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x00058B33 File Offset: 0x00056D33
		protected virtual void DescribeCustomFields(IScriptDescriptor descriptor)
		{
			if (this.CustomTaskFields.Count > 0)
			{
				descriptor.AddScriptProperty("customTaskFields", this.Serializer.Serialize(this.CustomTaskFields));
			}
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x00058B60 File Offset: 0x00056D60
		protected virtual void DescribeTasks(IScriptDescriptor descriptor)
		{
			IList<ITask> allTasks = this.GetAllTasks();
			if (allTasks.Count > 0)
			{
				descriptor.AddScriptProperty("tasksData", this.Serializer.Serialize(allTasks));
			}
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x00058B94 File Offset: 0x00056D94
		protected virtual void DescribeDependencies(IScriptDescriptor descriptor)
		{
			if (this.Dependencies.Count > 0)
			{
				descriptor.AddScriptProperty("dependenciesData", this.Serializer.Serialize(this.Dependencies));
			}
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x00058BC0 File Offset: 0x00056DC0
		protected virtual void DescribeResources(IScriptDescriptor descriptor)
		{
			if (this.Resources.Count > 0)
			{
				descriptor.AddScriptProperty("resourcesData", this.Serializer.Serialize(this.Resources));
			}
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00058BEC File Offset: 0x00056DEC
		protected virtual void DescribeAssignments(IScriptDescriptor descriptor)
		{
			if (this.Resources.Count > 0 && this.Assignments.Count > 0)
			{
				descriptor.AddScriptProperty("assignmentsData", this.Serializer.Serialize(this.Assignments));
			}
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00058C28 File Offset: 0x00056E28
		protected internal virtual void DescribeViews(IScriptDescriptor descriptor)
		{
			List<BaseViewSettings> list = new List<BaseViewSettings>();
			if (this.DayView.UserSelectable)
			{
				list.Add(this.DayView);
			}
			if (this.WeekView.UserSelectable)
			{
				list.Add(this.WeekView);
			}
			if (this.MonthView.UserSelectable)
			{
				list.Add(this.MonthView);
			}
			if (this.YearView.UserSelectable)
			{
				list.Add(this.YearView);
			}
			if (list.Count == 0)
			{
				throw new NotSupportedException("There is no view with UserSelectable set to true. RadGantt requires at least one view to have UserSelectable set to true");
			}
			descriptor.AddScriptProperty("viewsData", this.Serializer.Serialize(list));
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00058CC9 File Offset: 0x00056EC9
		protected internal virtual void DescribeColumns(IScriptDescriptor descriptor)
		{
			descriptor.AddScriptProperty("columnsData", this.Serializer.Serialize(this.VisibleColumns));
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00058CE8 File Offset: 0x00056EE8
		protected internal virtual void DescribeLocalization(IScriptDescriptor descriptor)
		{
			string text = this.Serializer.Serialize(this.Localization);
			if (!text.IsEmptySerializedObject())
			{
				descriptor.AddScriptProperty("localization", text);
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x00058D1B File Offset: 0x00056F1B
		internal static List<ColumnMetaData> DefaultColumns
		{
			get
			{
				return RadGantt.defaultColumns;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x00058D22 File Offset: 0x00056F22
		protected internal IColumnFactory DefaultColumnsFactory
		{
			get
			{
				if (this._defaultColumnsFactory == null)
				{
					this._defaultColumnsFactory = new DefaultColumnsFactory();
				}
				return this._defaultColumnsFactory;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x00058D45 File Offset: 0x00056F45
		protected internal IEnumerable<GanttBoundColumn> VisibleColumns
		{
			get
			{
				return from p in this.Columns
				where p.Visible
				select p;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x00058D6F File Offset: 0x00056F6F
		protected internal bool HasCustomProvider
		{
			get
			{
				return !(this.Provider is DataSourceViewGanttProvider);
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x00058D82 File Offset: 0x00056F82
		// (set) Token: 0x06001BEE RID: 7150 RVA: 0x00058D8A File Offset: 0x00056F8A
		protected internal new virtual bool RequiresDataBinding
		{
			get
			{
				return base.RequiresDataBinding;
			}
			set
			{
				base.RequiresDataBinding = value;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x00058D93 File Offset: 0x00056F93
		protected internal virtual bool DoExplicitDataBind
		{
			get
			{
				return !this.UsingWebServiceBinding && this.RequiresDataBinding && (this.HasCustomProvider || !base.IsBoundUsingDataSourceID);
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x00058DBC File Offset: 0x00056FBC
		protected internal JavaScriptSerializer Serializer
		{
			get
			{
				if (this._serializer == null)
				{
					this._serializer = new JavaScriptSerializer();
					this._serializer.MaxJsonLength = int.MaxValue;
					this._serializer.RegisterConverters(new JavaScriptConverter[]
					{
						new ColumnConverter(),
						new DayViewSettingsConverter(this),
						new WeekViewSettingsConverter(this),
						new MonthViewSettingsConverter(this),
						new YearViewSettingsConverter(this),
						new GanttKeyboardNavigationConverter(),
						new LocalizationConverter(),
						new TaskConverter(),
						new DependencyConverter(),
						new ResourceConverter(),
						new AssignmentConverter(),
						new LocalizationConverter(),
						new CustomFieldConverter(),
						new WebServiceSettingsConverter(),
						new ClientExportManagerPdfSettingsConverter(),
						new TasksTooltipConverter(),
						new GanttToolbarItemConverter()
					});
				}
				return this._serializer;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x00058E9D File Offset: 0x0005709D
		protected internal virtual DependenciesBinder DependenciesBinder
		{
			get
			{
				if (this._depBinder == null)
				{
					this._depBinder = new DependenciesBinder();
				}
				return this._depBinder;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00058EB8 File Offset: 0x000570B8
		protected internal virtual ResourcesBinder ResourcesBinder
		{
			get
			{
				if (this._resBinder == null)
				{
					this._resBinder = new ResourcesBinder();
				}
				return this._resBinder;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x00058ED3 File Offset: 0x000570D3
		protected internal virtual AssignmentsBinder AssignmentsBinder
		{
			get
			{
				if (this._asmBinder == null)
				{
					this._asmBinder = new AssignmentsBinder();
				}
				return this._asmBinder;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x00058EEE File Offset: 0x000570EE
		protected internal bool UsingWebServiceBinding
		{
			get
			{
				return this._webServiceSettings != null && !string.IsNullOrEmpty(this.WebServiceSettings.Path);
			}
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00058F10 File Offset: 0x00057110
		internal void ImportXml(XDocument xDocument)
		{
			List<Task> list = XmlImporter.ParseTasks(xDocument);
			foreach (Task task in list)
			{
				this.Provider.InsertTask(task);
			}
			List<Dependency> list2 = XmlImporter.ParseDependencies(xDocument);
			foreach (Dependency dependency in list2)
			{
				this.Provider.InsertDependency(dependency);
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x00058FB8 File Offset: 0x000571B8
		public DataSourceView TasksView
		{
			get
			{
				if (string.IsNullOrEmpty(this.DataSourceID) && this.DataSource == null)
				{
					throw new GanttDataSourceException("DataSourceID and DataSource may not be null in case of DataSource binding");
				}
				return this.GetData();
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00058FE0 File Offset: 0x000571E0
		public DataSourceView DependenciesView
		{
			get
			{
				if (string.IsNullOrEmpty(this.DependenciesDataSourceID) && this.DependenciesDataSource == null)
				{
					return null;
				}
				if (this._depBinderIDChanged)
				{
					this.UpdateBinder(this.DependenciesBinder, this.DependenciesDataSourceID);
					this._depBinderIDChanged = false;
				}
				return this.DependenciesBinder.GetData();
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x00059030 File Offset: 0x00057230
		public DataSourceView ResourcesView
		{
			get
			{
				if (string.IsNullOrEmpty(this.ResourcesDataSourceID) && this.ResourcesDataSource == null)
				{
					return null;
				}
				if (this._resBinderIDChanged)
				{
					this.UpdateBinder(this.ResourcesBinder, this.ResourcesDataSourceID);
					this._resBinderIDChanged = false;
				}
				return this.ResourcesBinder.GetData();
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00059080 File Offset: 0x00057280
		public DataSourceView AssignmentsView
		{
			get
			{
				if (string.IsNullOrEmpty(this.AssignmentsDataSourceID) && this.AssignmentsDataSource == null)
				{
					return null;
				}
				if (this._asmBinderIDChanged)
				{
					this.UpdateBinder(this.AssignmentsBinder, this.AssignmentsDataSourceID);
					this._asmBinderIDChanged = false;
				}
				return this.AssignmentsBinder.GetData();
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x000590D0 File Offset: 0x000572D0
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x000590D3 File Offset: 0x000572D3
		protected override string CssClassFormatString
		{
			get
			{
				return "RadGantt RadGantt_{0} radSkin_{0}";
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x000590DA File Offset: 0x000572DA
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000590DE File Offset: 0x000572DE
		protected override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.RenderDesignTimeHtml(writer);
				return;
			}
			base.Render(writer);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x0005910C File Offset: 0x0005730C
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
			List<ScriptReference> list = new List<ScriptReference>(scriptReferences);
			if (this.EnableEmbeddedScripts)
			{
				string fullName = Assembly.GetExecutingAssembly().FullName;
				if (this.EnableResources)
				{
					list.AddRange(new List<ScriptReference>
					{
						new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.list.js", fullName),
						new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.dropdownlist.js", fullName),
						new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.filtermenu.js", fullName),
						new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.pager.js", fullName),
						new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.grid.js", fullName)
					});
				}
				if (this.EnablePdfExport)
				{
					int index = list.FindIndex((ScriptReference c) => c.Name == "Telerik.Web.UI.Common.HTML5UI.html5.gantt.js");
					list.InsertRange(index, new List<ScriptReference>
					{
						new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.color.js", fullName),
						new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.drawing.js", fullName),
						new ScriptReference("Telerik.Web.UI.Common.HTML5UI.html5.pdf.js", fullName)
					});
				}
			}
			return list;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0005921C File Offset: 0x0005741C
		protected override void CreateChildControls()
		{
			this._dataPropertyChanged = false;
			this.CreateColumns();
			if (!string.IsNullOrEmpty(this.DependenciesDataSourceID))
			{
				this.CreateBinder(this.DependenciesBinder);
			}
			if (!string.IsNullOrEmpty(this.ResourcesDataSourceID))
			{
				this.CreateBinder(this.ResourcesBinder);
			}
			if (!string.IsNullOrEmpty(this.AssignmentsDataSourceID))
			{
				this.CreateBinder(this.AssignmentsBinder);
			}
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00059281 File Offset: 0x00057481
		protected virtual void CreateBinder(DataBoundControl binder)
		{
			this.Controls.Add(binder);
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0005928F File Offset: 0x0005748F
		protected virtual void UpdateBinder(DataBoundControl binder, string dataSourceID)
		{
			if (binder.Parent == null)
			{
				this.CreateBinder(binder);
			}
			binder.DataSourceID = dataSourceID;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000592A8 File Offset: 0x000574A8
		protected override void PerformSelect()
		{
			if (!base.DesignMode)
			{
				this.RequiresDataBinding = false;
				this.OnDataBinding(EventArgs.Empty);
				this.BindDependencies(this.Provider.GetDependencies());
				this.BindTasks(this.Provider.GetTasks());
				if (this.EnableResources)
				{
					this.BindResources(this.Provider.GetResources());
					this.BindAssignments(this.Provider.GetAssignments());
				}
				this.OnDataBound(EventArgs.Empty);
			}
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00059326 File Offset: 0x00057526
		protected virtual void BindTasks(IEnumerable<ITask> tasks)
		{
			this.Tasks.Clear();
			this.Tasks.AddRange(tasks);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0005933F File Offset: 0x0005753F
		protected virtual void BindDependencies(IEnumerable<IDependency> dependencies)
		{
			this.Dependencies.Clear();
			this.Dependencies.AddRange(dependencies);
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00059358 File Offset: 0x00057558
		protected virtual void BindResources(IEnumerable<IResource> resources)
		{
			this.Resources.Clear();
			this.Resources.AddRange(resources);
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x00059371 File Offset: 0x00057571
		protected virtual void BindAssignments(IEnumerable<IAssignment> assignments)
		{
			this.Assignments.Clear();
			this.Assignments.AddRange(assignments);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x0005938A File Offset: 0x0005758A
		protected override void OnDataPropertyChanged()
		{
			base.OnDataPropertyChanged();
			this._dataPropertyChanged = true;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x0005939C File Offset: 0x0005759C
		protected override void OnPreRender(EventArgs e)
		{
			if (this._dataPropertyChanged)
			{
				this.EnsureDataBound();
				this.EnsureChildControls();
			}
			base.OnPreRender(e);
			if (this.Culture.Name != "en-US")
			{
				string script = this.Culture.Format();
				ScriptManager.RegisterStartupScript(this.Page, typeof(RadGantt), "GanttCultureScript", script, true);
			}
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00059403 File Offset: 0x00057603
		protected override void EnsureDataBound()
		{
			base.EnsureDataBound();
			if (this.DoExplicitDataBind)
			{
				this.DataBind();
			}
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0005941C File Offset: 0x0005761C
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			GanttClientState ganttClientState = javaScriptSerializer.Deserialize<GanttClientState>(text);
			this.ScrollTop = ganttClientState.ScrollTop;
			this.ScrollLeft = ganttClientState.ScrollLeft;
			this.SelectedView = ganttClientState.SelectedView;
			return false;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00059474 File Offset: 0x00057674
		public string GetCallbackResult()
		{
			string result = string.Empty;
			if (this._modifiedDependencies.Count > 0)
			{
				result = this.Serializer.Serialize(this._modifiedDependencies);
			}
			else if (this._modifiedAssignments.Count > 0)
			{
				result = this.Serializer.Serialize(this._modifiedAssignments);
			}
			else
			{
				result = this.Serializer.Serialize(this._modifiedTasks);
			}
			return result;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000594E0 File Offset: 0x000576E0
		public void RaiseCallbackEvent(string eventArgument)
		{
			ICallbackCommandContext cmd = CallbackCommand.FromEventArgument(eventArgument, this.Provider.TaskFactory);
			this.FireCommand(cmd);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x000596C8 File Offset: 0x000578C8
		protected internal virtual void FireCommand(ICallbackCommandContext cmd)
		{
			this._modifiedTasks = new List<ITask>();
			this._modifiedDependencies = new List<IDependency>();
			this._modifiedAssignments = new List<IAssignment>();
			Action<Func<ITask, ITask>> action4 = delegate(Func<ITask, ITask> action)
			{
				foreach (ITask arg in cmd.Tasks)
				{
					this._modifiedTasks.Add(action(arg));
				}
			};
			Action<Func<IDependency, IDependency>> action2 = delegate(Func<IDependency, IDependency> action)
			{
				foreach (IDependency arg in cmd.Dependencies)
				{
					this._modifiedDependencies.Add(action(arg));
				}
			};
			Action<Func<IAssignment, IAssignment>> action3 = delegate(Func<IAssignment, IAssignment> action)
			{
				foreach (IAssignment arg in cmd.Assignments)
				{
					this._modifiedAssignments.Add(action(arg));
				}
			};
			switch (cmd.Command)
			{
			case CommandType.UpdateTask:
				action4((ITask task) => this.Provider.UpdateTask(task));
				return;
			case CommandType.DeleteTask:
				action4((ITask task) => this.Provider.DeleteTask(task));
				return;
			case CommandType.InsertTask:
				action4((ITask task) => this.Provider.InsertTask(task));
				return;
			case CommandType.UpdateDependency:
				action2((IDependency dependency) => this.Provider.UpdateDependency(dependency));
				return;
			case CommandType.DeleteDependency:
				action2((IDependency dependency) => this.Provider.DeleteDependency(dependency));
				return;
			case CommandType.InsertDependency:
				action2((IDependency dependency) => this.Provider.InsertDependency(dependency));
				return;
			case CommandType.UpdateAssignment:
				action3((IAssignment assignment) => this.Provider.UpdateAssignment(assignment));
				return;
			case CommandType.DeleteAssignment:
				action3((IAssignment assignment) => this.Provider.DeleteAssignment(assignment));
				return;
			case CommandType.InsertAssignment:
				action3((IAssignment assignment) => this.Provider.InsertAssignment(assignment));
				return;
			default:
				return;
			}
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x0005987A File Offset: 0x00057A7A
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00059884 File Offset: 0x00057A84
		public void RaisePostBackEvent(string eventArgument)
		{
			IPostbackCommandContext cmd = PostbackCommand.FromEventArgument(eventArgument, this.Provider.TaskFactory);
			this.FirePostbackCommand(cmd);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00059924 File Offset: 0x00057B24
		protected internal virtual void FirePostbackCommand(IPostbackCommandContext cmd)
		{
			switch (cmd.Command)
			{
			case CommandType.SwitchToDayView:
				this.SelectedView = GanttViewType.DayView;
				this.OnNavigationCommand(new NavigationCommandEventArgs(GanttNavigationCommand.SwitchToDayView));
				return;
			case CommandType.SwitchToWeekView:
				this.SelectedView = GanttViewType.WeekView;
				this.OnNavigationCommand(new NavigationCommandEventArgs(GanttNavigationCommand.SwitchToWeekView));
				return;
			case CommandType.SwitchToMonthView:
				this.SelectedView = GanttViewType.MonthView;
				this.OnNavigationCommand(new NavigationCommandEventArgs(GanttNavigationCommand.SwitchToMonthView));
				return;
			case CommandType.SwitchToYearView:
				this.SelectedView = GanttViewType.YearView;
				this.OnNavigationCommand(new NavigationCommandEventArgs(GanttNavigationCommand.SwitchToYearView));
				return;
			case CommandType.Postback:
				if (cmd.InsertedTasks.Count > 0)
				{
					TaskEventArgs taskEventArgs = new TaskEventArgs(cmd.InsertedTasks);
					this.OnTaskInsert(taskEventArgs);
					if (!taskEventArgs.Cancel)
					{
						cmd.InsertedTasks.ForEach(delegate(ITask task)
						{
							this.Provider.InsertTask(task);
						});
					}
				}
				if (cmd.UpdatedTasks.Count > 0)
				{
					TaskEventArgs taskEventArgs2 = new TaskEventArgs(cmd.UpdatedTasks);
					this.OnTaskUpdate(taskEventArgs2);
					if (!taskEventArgs2.Cancel)
					{
						cmd.UpdatedTasks.ForEach(delegate(ITask task)
						{
							this.Provider.UpdateTask(task);
						});
					}
				}
				if (cmd.DeletedTasks.Count > 0)
				{
					TaskEventArgs taskEventArgs3 = new TaskEventArgs(cmd.DeletedTasks);
					this.OnTaskDelete(taskEventArgs3);
					if (!taskEventArgs3.Cancel)
					{
						cmd.DeletedTasks.ForEach(delegate(ITask task)
						{
							this.Provider.DeleteTask(task);
						});
					}
				}
				if (cmd.InsertedDependencies.Count > 0)
				{
					DependencyEventArgs dependencyEventArgs = new DependencyEventArgs(cmd.InsertedDependencies);
					this.OnDependencyInsert(dependencyEventArgs);
					if (!dependencyEventArgs.Cancel)
					{
						cmd.InsertedDependencies.ForEach(delegate(IDependency dependency)
						{
							this.Provider.InsertDependency(dependency);
						});
					}
				}
				if (cmd.DeletedDependencies.Count > 0)
				{
					DependencyEventArgs dependencyEventArgs2 = new DependencyEventArgs(cmd.DeletedDependencies);
					this.OnDependencyDelete(dependencyEventArgs2);
					if (!dependencyEventArgs2.Cancel)
					{
						cmd.DeletedDependencies.ForEach(delegate(IDependency dependency)
						{
							this.Provider.DeleteDependency(dependency);
						});
					}
				}
				if (this.EnableResources)
				{
					if (cmd.InsertedAssignments.Count > 0)
					{
						AssignmentEventArgs assignmentEventArgs = new AssignmentEventArgs(cmd.InsertedAssignments);
						this.OnAssignmentInsert(assignmentEventArgs);
						if (!assignmentEventArgs.Cancel)
						{
							cmd.InsertedAssignments.ForEach(delegate(IAssignment assignment)
							{
								this.Provider.InsertAssignment(assignment);
							});
						}
					}
					if (cmd.UpdatedAssignments.Count > 0)
					{
						AssignmentEventArgs assignmentEventArgs2 = new AssignmentEventArgs(cmd.UpdatedAssignments);
						this.OnAssignmentUpdate(assignmentEventArgs2);
						if (!assignmentEventArgs2.Cancel)
						{
							cmd.UpdatedAssignments.ForEach(delegate(IAssignment assignment)
							{
								this.Provider.UpdateAssignment(assignment);
							});
						}
					}
					if (cmd.DeletedAssignments.Count > 0)
					{
						AssignmentEventArgs assignmentEventArgs3 = new AssignmentEventArgs(cmd.DeletedAssignments);
						this.OnAssignmentDelete(assignmentEventArgs3);
						if (!assignmentEventArgs3.Cancel)
						{
							cmd.DeletedAssignments.ForEach(delegate(IAssignment assignment)
							{
								this.Provider.DeleteAssignment(assignment);
							});
						}
					}
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00059C18 File Offset: 0x00057E18
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "allowColumnResize", this.AllowColumnResize, false);
			base.DescribeProperty<bool>(descriptor, "allowDependencyDelete", this.AllowDependencyDelete, true);
			base.DescribeProperty<bool>(descriptor, "allowDependencyInsert", this.AllowDependencyInsert, true);
			base.DescribeProperty<bool>(descriptor, "allowPercentCompleteDrag", this.AllowPercentCompleteDrag, true);
			base.DescribeProperty<bool>(descriptor, "allowTaskDelete", this.AllowTaskDelete, true);
			base.DescribeProperty<bool>(descriptor, "allowTaskInsert", this.AllowTaskInsert, true);
			base.DescribeProperty<bool>(descriptor, "allowPlannedTasks", this.AllowPlannedTasks, false);
			base.DescribeProperty<bool>(descriptor, "showPlannedTasks", this.ShowPlannedTasks, false);
			base.DescribeProperty<bool>(descriptor, "allowTaskMove", this.AllowTaskMove, true);
			base.DescribeProperty<bool>(descriptor, "allowTaskReorder", this.AllowTaskReorder, true);
			base.DescribeProperty<bool>(descriptor, "allowTaskResize", this.AllowTaskResize, true);
			base.DescribeProperty<bool>(descriptor, "allowTaskUpdate", this.AllowTaskUpdate, true);
			base.DescribeProperty<string>(descriptor, "clientTemplate", this.ClientTemplate, "");
			base.DescribeProperty<int>(descriptor, "currentTimeMarkerInterval", this.CurrentTimeMarkerInterval, 10000);
			base.DescribeProperty<bool>(descriptor, "displayDeleteConfirmation", this.DisplayDeleteConfirmation, true);
			base.DescribeProperty<bool>(descriptor, "enablePdfExport", this.EnablePdfExport, false);
			base.DescribeProperty<bool>(descriptor, "enableResources", this.EnableResources, false);
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "700px");
			base.DescribeProperty<string>(descriptor, "listWidth", this.ListWidth.ToString(CultureInfo.InvariantCulture), "30%");
			base.DescribeProperty<DateTime?>(descriptor, "rangeEnd", this.RangeEnd, null);
			base.DescribeProperty<DateTime?>(descriptor, "rangeStart", this.RangeStart, null);
			base.DescribeProperty<bool>(descriptor, "readOnly", this.ReadOnly, false);
			base.DescribeProperty<string>(descriptor, "rowHeight", this.RowHeight.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<int>(descriptor, "scrollLeft", this.ScrollLeft, 0);
			base.DescribeProperty<int>(descriptor, "scrollTop", this.ScrollTop, 0);
			base.DescribeProperty<DateTime?>(descriptor, "selectedDate", this.SelectedDate, null);
			base.DescribeProperty<bool>(descriptor, "showCurrentTimeMarker", this.ShowCurrentTimeMarker, true);
			base.DescribeProperty<bool>(descriptor, "showFullTime", this.ShowFullTime, false);
			base.DescribeProperty<bool>(descriptor, "showFullWeek", this.ShowFullWeek, true);
			base.DescribeProperty<bool>(descriptor, "snapToGrid", this.SnapToGrid, true);
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "100%");
			base.DescribeProperty<DayOfWeek>(descriptor, "workWeekEnd", this.WorkWeekEnd, DayOfWeek.Friday);
			base.DescribeProperty<DayOfWeek>(descriptor, "workWeekStart", this.WorkWeekStart, DayOfWeek.Monday);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00059F08 File Offset: 0x00058108
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "columnResized", this.OnClientColumnResized);
			RadDataBoundControl.DescribeEvent(descriptor, "dataBound", this.OnClientDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "kendoWidgetInitializing", this.OnClientKendoWidgetInitializing);
			RadDataBoundControl.DescribeEvent(descriptor, "navigationCommand", this.OnClientNavigationCommand);
			RadDataBoundControl.DescribeEvent(descriptor, "pdfExporting", this.OnClientPdfExporting);
			RadDataBoundControl.DescribeEvent(descriptor, "requestStart", this.OnClientRequestStart);
			RadDataBoundControl.DescribeEvent(descriptor, "requestEnd", this.OnClientRequestEnd);
			RadDataBoundControl.DescribeEvent(descriptor, "requestFailed", this.OnClientRequestFailed);
			RadDataBoundControl.DescribeEvent(descriptor, "taskMoveEnd", this.OnClientTaskMoveEnd);
			RadDataBoundControl.DescribeEvent(descriptor, "taskMoveStart", this.OnClientTaskMoveStart);
			RadDataBoundControl.DescribeEvent(descriptor, "taskMoving", this.OnClientTaskMoving);
			RadDataBoundControl.DescribeEvent(descriptor, "taskResizeEnd", this.OnClientTaskResizeEnd);
			RadDataBoundControl.DescribeEvent(descriptor, "taskResizeStart", this.OnClientTaskResizeStart);
			RadDataBoundControl.DescribeEvent(descriptor, "taskResizing", this.OnClientTaskResizing);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "togglePlannedTasks", this.OnClientTogglePlannedTasks);
			RadDataBoundControl.DescribeEvent(descriptor, "taskSelectionChanged", this.OnClientTaskSelectionChanged);
			RadDataBoundControl.DescribeEvent(descriptor, "taskEditing", this.OnClientTaskEditing);
			RadDataBoundControl.DescribeEvent(descriptor, "taskSaving", this.OnClientTaskSaving);
			RadDataBoundControl.DescribeEvent(descriptor, "taskUpdateCancel", this.OnClientTaskUpdateCancel);
			RadDataBoundControl.DescribeEvent(descriptor, "inserting", this.OnClientInserting);
			RadDataBoundControl.DescribeEvent(descriptor, "removing", this.OnClientRemoving);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040006FB RID: 1787
		private GanttProviderBase _tasksProvider;

		// Token: 0x040006FC RID: 1788
		private TaskCollection _tasksCollection;

		// Token: 0x040006FD RID: 1789
		private DependencyCollection _dependenciesCollection;

		// Token: 0x040006FE RID: 1790
		private ResourceCollection _resourcesCollection;

		// Token: 0x040006FF RID: 1791
		private AssignmentCollection _assignmentsCollection;

		// Token: 0x04000700 RID: 1792
		private ColumnCollection _columnsCollection;

		// Token: 0x04000701 RID: 1793
		private CustomFieldCollection _customTaskFields;

		// Token: 0x04000702 RID: 1794
		private WebServiceSettings _webServiceSettings;

		// Token: 0x04000703 RID: 1795
		private static readonly object columnCreatingEvent = new object();

		// Token: 0x04000704 RID: 1796
		private static readonly object columnCreatedEvent = new object();

		// Token: 0x04000705 RID: 1797
		private static readonly object taskInsertEvent = new object();

		// Token: 0x04000706 RID: 1798
		private static readonly object taskUpdateEvent = new object();

		// Token: 0x04000707 RID: 1799
		private static readonly object taskDeleteEvent = new object();

		// Token: 0x04000708 RID: 1800
		private static readonly object dependencyInsertEvent = new object();

		// Token: 0x04000709 RID: 1801
		private static readonly object dependencyDeleteEvent = new object();

		// Token: 0x0400070A RID: 1802
		private static readonly object assignmentInsertEvent = new object();

		// Token: 0x0400070B RID: 1803
		private static readonly object assignmentUpdateEvent = new object();

		// Token: 0x0400070C RID: 1804
		private static readonly object assignmentDeleteEvent = new object();

		// Token: 0x0400070D RID: 1805
		private static readonly object navigationCommandEvent = new object();

		// Token: 0x0400070E RID: 1806
		private FilterEntryCollection _entries;

		// Token: 0x0400070F RID: 1807
		private TasksTooltip _tasksTooltip;

		// Token: 0x04000710 RID: 1808
		private GanttToolbar _toolbar;

		// Token: 0x04000711 RID: 1809
		private JavaScriptSerializer _serializer;

		// Token: 0x04000712 RID: 1810
		private DependenciesBinder _depBinder;

		// Token: 0x04000713 RID: 1811
		private ResourcesBinder _resBinder;

		// Token: 0x04000714 RID: 1812
		private AssignmentsBinder _asmBinder;

		// Token: 0x04000715 RID: 1813
		private GanttDataBindings _bindings;

		// Token: 0x04000716 RID: 1814
		private GanttStrings _localization;

		// Token: 0x04000717 RID: 1815
		private IColumnFactory _defaultColumnsFactory;

		// Token: 0x04000718 RID: 1816
		private DayViewSettings _dayViewSettings;

		// Token: 0x04000719 RID: 1817
		private WeekViewSettings _weekViewSettings;

		// Token: 0x0400071A RID: 1818
		private MonthViewSettings _monthViewSettings;

		// Token: 0x0400071B RID: 1819
		private YearViewSettings _yearViewSettings;

		// Token: 0x0400071C RID: 1820
		private GanttExportSettings _exportSettings;

		// Token: 0x0400071D RID: 1821
		private GanttKeyboardNavigationSettings _keyboardNavigationSettings;

		// Token: 0x0400071E RID: 1822
		private bool _dataPropertyChanged;

		// Token: 0x0400071F RID: 1823
		private bool _depBinderIDChanged;

		// Token: 0x04000720 RID: 1824
		private bool _resBinderIDChanged;

		// Token: 0x04000721 RID: 1825
		private bool _asmBinderIDChanged;

		// Token: 0x04000722 RID: 1826
		private static readonly List<ColumnMetaData> defaultColumns = new List<ColumnMetaData>
		{
			new ColumnMetaData
			{
				DataField = "id",
				Title = "ID",
				Type = DataType.Number
			},
			new ColumnMetaData
			{
				DataField = "title",
				Title = "Title",
				Type = DataType.String
			},
			new ColumnMetaData
			{
				DataField = "start",
				Title = "Start Time",
				Type = DataType.DateTime
			},
			new ColumnMetaData
			{
				DataField = "end",
				Title = "End Time",
				Type = DataType.DateTime
			},
			new ColumnMetaData
			{
				DataField = "percentComplete",
				Title = "Percent Complete",
				Type = DataType.Number
			}
		};

		// Token: 0x04000723 RID: 1827
		private IList<ITask> _modifiedTasks;

		// Token: 0x04000724 RID: 1828
		private IList<IDependency> _modifiedDependencies;

		// Token: 0x04000725 RID: 1829
		private IList<IAssignment> _modifiedAssignments;
	}
}
