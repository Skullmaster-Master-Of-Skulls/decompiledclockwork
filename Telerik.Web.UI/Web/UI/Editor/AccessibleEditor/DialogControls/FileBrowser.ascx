<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Widgets" TagPrefix="widgets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Dialogs" TagPrefix="dialogs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script type="text/javascript">
	Type.registerNamespace("Telerik.Web.UI.Editor.DialogControls");

	Telerik.Web.UI.Editor.DialogControls.FileBrowser = function (element)
	{
		Telerik.Web.UI.Editor.DialogControls.FileBrowser.initializeBase(this, [element]);
	}

	Telerik.Web.UI.Editor.DialogControls.FileBrowser.prototype = {
		initialize: function ()
		{
			this.set_insertButton($get("InsertButton"));
			this.set_cancelButton($get("CancelButton"));

			var previewer = this.get_previewerType();
			var previewerType = eval("Telerik.Web.UI.Widgets." + previewer);
			$create(previewerType, { "browser": this }, null, null, $get(previewer));
			this.set_filePreviewer($find(previewer));
			this.set_fileBrowser($find("RadFileExplorer1"));

			Telerik.Web.UI.Editor.DialogControls.FileBrowser.callBaseMethod(this, 'initialize');
		},

		dispose: function ()
		{
			Telerik.Web.UI.Editor.DialogControls.FileBrowser.callBaseMethod(this, 'dispose');
			this._insertButton = null;
			this._cancelButton = null;
		}
	}

	Telerik.Web.UI.Editor.DialogControls.FileBrowser.registerClass('Telerik.Web.UI.Editor.DialogControls.FileBrowser', Telerik.Web.UI.Widgets.FileManager);
</script>
<div style="display: none;">
	<telerik:RadFileExplorer ID="RadFileExplorer1" OnClientLoad="OnClientLoad" VisibleControls="AddressBox,TreeView"
		ExplorerMode="FileTree" runat="Server" EnableOpenFile="false" AllowPaging="true"
		EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" />
	<asp:PlaceHolder ID="PreviewerPlaceHolder" runat="server" />
	<button type="button" id="InsertButton">
		hidden insert button</button>
	<button type="button" id="CancelButton">
		hidden cancel button</button>
</div>
<style type="text/css">
	.floatLeft
	{
		float: left;
		padding: 3px;
	}
	label
	{
		font-weight: bold;
	}
	.bottomButton
	{
		margin-top: 20px;
	}
</style>
<script type="text/javascript">
	//Override RadFormDecorator method that performs decoration
	Telerik.Web.UI.RadFormDecorator.prototype.decorate = function () { };

	function OnClientLoad(sender, args)
	{
		var explorer = new AccessibleFileExplorer(sender);
	}

	//RNIB logic - load folder and its sublfolders to the left, files to the right
	var AccessibleFileExplorer = function (sender)
	{
		//Get reference to the file explorer's treeview
		var tree = sender.get_tree();

		//Set propertiess
		this._fileExplorer = sender;
		this._treeView = tree;

		//Localization strings  
		this._rootString = "/";
		this._noFilesString = "No files in folder";
		this._noFileSelectedString = "No file selected";
		this._noFolderSelectedString = "No folder selected";
		this._contentsOfString = "File selection: contents of ";
		this._noneSelectedString = "None selected";
		this._parentFolderString = "back to parent";

		//Selects
		this._leftSelect = $get("leftSelect");
		this._rightSelect = $get("rightSelect");
		this._topSelect = $get("topSelect");

		//Hook topSelect to an onchange event
		this._configureSelect(this._topSelect, Function.createDelegate(this, this._topSelectOnChange));

		//Label elements
		this._currentFolderLabel = $get("currentFolderSpan");
		if (!this._currentFolderLabel) this._currentFolderLabel = document.createElement("SPAN");
		this._startFolderLabel = $get("startFolderSpan");
		if (!this._startFolderLabel) this._startFolderLabel = document.createElement("SPAN");

		this._fileSelectionLabel = $get("fileSelectionLabel");
		if (!this._fileSelectionLabel) this._fileSelectionLabel = document.createElement("SPAN");

		//openFolderButton
		this._openFolderButton = $get("openFolderButton");
		if (this._openFolderButton)
		{
			$addHandler(this._openFolderButton, "click", Function.createDelegate(this, this._openSelectedFolder));
		}

		//Configure the Select file button
		var selectFileButton = $get("selectFileButton");
		if (selectFileButton)
		{
			$addHandler(selectFileButton, "click", Function.createDelegate(this, this._selectFile));
		}

		//Configure the Load files button
		var loadFilesButton = $get("loadFilesButton");
		if (loadFilesButton)
		{
			$addHandler(loadFilesButton, "click", Function.createDelegate(this, this._loadRightPane));
		}

		//When a folder loads, load its contents in the select element
		sender.add_folderLoaded(Function.createDelegate(this, this._onFolderLoaded));

		//Disable multiple select - this will simplify code later on!
		tree.set_multipleSelect(false);

		//There needs to be checked whether treeview has multiple roots or not
		//If there are multiple roots like in MOSS, then create a fake root node and add all root nodes beneath
		var hasMultipleRoots = this._hasMultipleRoots();
		if (hasMultipleRoots)
		{
			tree.trackChanges();
			//Instantiate a new client node
			var node = new Telerik.Web.UI.RadTreeNode();
			//Set text and value text
			node.set_text(this._rootString);
			node.set_value(this._rootString);

			//Move all other nodes to this one
			var nodes = tree.get_nodes();
			while (curNode = nodes.getNode(0))
			{
				nodes.remove(curNode);
				node.get_nodes().add(curNode);
			}

			tree.get_nodes().add(node);
			tree.commitChanges();
			node.set_expanded(true);
			node.select();
		}

		var rootNode = tree.get_selectedNode();

		//Set starting folder label - this never changes once it is set
		if (rootNode)
		{
			this._startFolderLabel.innerHTML = this._getFullFolderName(rootNode, true);

			//Display folders in left pane - which in term will display files in right pane
			this._fillLeftPane(rootNode);
		}
	};

	AccessibleFileExplorer.prototype =
{
	_hasMultipleRoots: function ()
	{
		var treeNodes = this._treeView.get_nodes();
		var count = treeNodes.get_count();
		return (count > 1);
	},

	//Format is /ROOT/folder1/folder2/
	//Due to the bgi differences between RadFileExlorer and the accessible interface, this function does a number of tricks
	_getFullFolderName: function (treeNode, addTrailingSlash)
	{

		var folderName = treeNode.get_value();
		//alert(folderName + " haa " + folderName.charAt(0));
		if (folderName.charAt(0) != "/") folderName = "/" + folderName;

		if (addTrailingSlash && folderName != "/") folderName += "/";
		return folderName;
	},

	_loadRightPane: function ()
	{
		var leftSelect = this._leftSelect;
		if (leftSelect.selectedIndex < 0)
		{
			alert(this._noFolderSelectedString);
			return;
		}

		var option = leftSelect[leftSelect.selectedIndex];
		var value = option ? option.value : null;
		if (value)
		{
			var node = this._treeView.findNodeByValue(value);
			if (node)
			{
				//Determine if node already expanded ever, or needs to load
				if (node.get_expandMode() == Telerik.Web.UI.TreeNodeExpandMode.ServerSideCallBack)
				{
					node.expand(); //Folder has not been loaded by callback yet. Initiate callback
				}
				//Folder is cached and (right-side files) can/should be displayed immediately
				else this._fillRightPane(node);
			}
		}
	},

	_fillRightPane: function (node)
	{
		this._loadItemsInPane(node, this._rightSelect, false);

		//Change "File selection" label
		this._fileSelectionLabel.innerHTML = this._contentsOfString + this._getFullFolderName(node);
	},

	_onFolderLoaded: function (sender, args)
	{
		var tree = this._treeView;
		var loadedNode = tree.findNodeByValue(args.get_path());
		var selectedNode = tree.get_selectedNode();
		//Not always the loaded node is the selected note. There is the common "preview" case where user simply browses folders on the left and wants to see their files on the right
		if (loadedNode != selectedNode)
		{
			this._loadRightPane();
		}
		else
		{
			this._fillLeftPane(loadedNode);
		}
	},

	_openSelectedFolder: function ()
	{
		var option = this._topSelect.options[this._topSelect.selectedIndex];
		if (option)
		{
			var folderToLoad = option.value;
			if (folderToLoad == this._noneSelectedString) return;

			//Load the new folder in the right folder
			var tree = this._treeView;
			var node = tree.findNodeByValue(folderToLoad);
			if (node)
			{
				//Determine if node already expanded ever, or needs to load
				if (node.get_expandMode() == Telerik.Web.UI.TreeNodeExpandMode.ServerSideCallBack)
				{
					node.expand(); //Folder has not been loaded by callback yet. Initiate callback
					//Select folder because _onFolderLoaded depends on this!
					var oldSelect = tree.get_selectedNode();
					if (oldSelect) oldSelect.set_selected(false);
					node.select();
				}
				else this._fillLeftPane(node); //Folder is cached and (right-side files) can/should be displayed immediately
			}
		}
	},

	_topSelectOnChange: function ()
	{
		//Enable the button - however, if first selected item is "None selected" do nothing
		var select = this._topSelect;
		if (select.selectedIndex == 0 && select.options[0].value == this._noneSelectedString)
		{
			this._openFolderButton.disabled = true;
		}
		else this._openFolderButton.disabled = false;
	},

	_fillLeftPane: function (treeNode)
	{
		//Fill the select with the root tree files
		this._loadItemsInPane(treeNode, this._leftSelect, true);

		//Set label with the current folder
		this._currentFolderLabel.innerHTML = this._getFullFolderName(treeNode, true);

		//Fill-in the "Navigate to subfolder" select and enable the "Open selected folder" button
		//EXTRA CONDITIONS - if folder is root make sure you display all folders properly - left pane does not have the "first/root" folder listed!
		var folderLength = this._leftSelect.options.length;
		if (folderLength > 0)
		{
			var topSelect = this._topSelect;

			//1) Clear items & set no selected item
			topSelect.options.length = 0;
			topSelect.selectedIndex = -1;

			//2 Folder names are listed only as names in the treeview, so use this
			//If folder is root, add None selected, else add Back to parent
			if (treeNode.get_level() == 0)
			{
				var option = new Option(this._noneSelectedString, this._noneSelectedString);
				topSelect.options[0] = option;
			}
			else
			{
				//Set parent folder as path to the "back to parent" option
				var parentFolder = treeNode.get_parent().get_value();
				var option = new Option(this._parentFolderString, parentFolder);
				topSelect.options[0] = option;
			}

			//Add child folders to the folder
			var explorer = this._fileExplorer;
			var treeNodes = treeNode.get_nodes();
			var count = treeNodes.get_count();
			for (var i = 0; i < count; i++)
			{
				var node = treeNodes.getNode(i);
				var item = explorer._createItemFromTreeViewNode(node);
				if (item.isDirectory())
				{
					topSelect.options[topSelect.options.length] = new Option(node.get_text(), node.get_value());
				}
			}
		}

		//Alwasys disable the button, it will be enabled only when user selects option from topSelect
		this._openFolderButton.disabled = true;

		//Load files in the right pane
		this._loadRightPane();
	},

	//justFolders == true -> display folder name itself, as well as direct subfolders
	_loadItemsInPane: function (treeNode, select, justFolders)
	{
		//Remove all existing nodes
		select.options.length = 0;

		//If you need to display folders only, start with the treeNode's name itself
		//However, it needs to be added to the treeview only if the treeNode is not a root folder itself

		var folderName = ""; //Needed later in the code!
		if (justFolders && (treeNode != this._treeView))
		{
			var folderPath = treeNode.get_value();

			//Load the folder name appropaitely - prepend ../ if not a root level
			if (treeNode.get_level() > 0)
			{
				folderName = "../" + treeNode.get_text() + "/";
			}
			else
			{
				folderName = this._getFullFolderName(treeNode, true);
			}

			var option = new Option(folderName, folderPath);
			select.options[0] = option;
		}

		//Display direct subfolders! Add the direct folder nodes - with proper formatting of the text
		var explorer = this._fileExplorer;
		var treeNodes = treeNode.get_nodes();
		var count = treeNodes.get_count();

		for (var i = 0; i < count; i++)
		{
			var node = treeNodes.getNode(i);
			var item = explorer._createItemFromTreeViewNode(node);
			var isFolder = item.isDirectory();

			var value = node.get_value();
			var option = null;
			if (justFolders && isFolder)
			{
				//Show parent folder in the name
				var text = folderName + node.get_text();
				option = new Option(text, value);
			}
			else if (!justFolders && !isFolder)
			{
				var text = node.get_text();
				option = new Option(text, value);
			}

			//Add the option
			if (option) select.options[select.options.length] = option;
		}

		//Check if folder has no files, and let the user know
		if (!justFolders && select.options.length == 0)
		{
			var option = new Option(this._noFilesString, this._noFilesString); //text, value
			select.options[select.options.length] = option;
		}

		//Set the selected index to the first item
		select.selectedIndex = 0;

		//Set focus to the select - in case it was loaded from a callback
		window.setTimeout(function ()
		{
			select.focus();
		}, 100);
	},

	//The "Select file" button calls this method
	_selectFile: function ()
	{
		var tree = this._treeView;
		var rightSelect = this._rightSelect;
		if (rightSelect.selectedIndex < 0)
		{
			alert(this._noFileSelectedString);
			return;
		}

		var value = rightSelect[rightSelect.selectedIndex].value;
		var node = tree.findNodeByValue(value);

		//It can be that no file was selected - e.g. when the folder only contains the "No files in folder" message
		if (!node)
		{
			alert(this._noFileSelectedString);
			return;
		}

		//select file in the tree
		tree.unselectAllNodes();
		node.select();
		var eventArgs = new Telerik.Web.UI.RadTreeNodeEventArgs(node);
		tree.raiseEvent("nodeClicked", eventArgs);

		//Programmmatically "click" the insert button - for some reason it needs a timeout!
		window.setTimeout(function ()
		{
			$get("InsertButton").click();
		}, 100);
	},

	//NEW: RNIB
	_configureSelect: function (select, callBack)
	{
		var sClicked = false;
		select.onclick = function ()
		{
			if (sClicked == true)
			{
				callBack();
			} else
			{
				if (select.click) select.click();
				sClicked = true;
			}
		};

		select.onkeydown = function (evt)
		{
			if (!evt) evt = window.event;
			if (evt.keyCode == 13)
			{
				callBack();
				//sClicked = false;
			}
			else return true;
		}
	},
	//not used for the time being
	closeDialog: function ()
	{
		var cancel = $get("CancelButton");
		if (cancel) cancel.click();
	}
};
</script>
<div class="floatLeft">
	<br />
	<label for="startFolderSpan">
		Starting folder</label>: <span id="startFolderSpan"></span>
	<br />
	<label for="currentFolderSpan">
		Current folder</label>: <span id="currentFolderSpan"></span>
	<br />
	<div class="floatLeft" style="width: 98%;">
		<label style="margin-left: 155px" for="topSelect">
			Navigate to subfolder:</label>
		<select style="width: 200px" id="topSelect">
		</select>
		<input type="button" id="openFolderButton" value="Open selected folder" disabled="disabled" />
	</div>
	<div class="floatLeft" style="width: 150px;">
		<label for="leftSelect">
			Folder selection:</label>
		<select style="width: 150px;" id="leftSelect" size="9">
		</select>
		<input class="bottomButton" type="button" id="loadFilesButton" value="Load files" />
	</div>
	<div class="floatLeft" style="width: 510px;">
		<label id="fileSelectionLabel" for="rightSelect">
			File selection: contents of
		</label>
		<span id="cur"></span>
		<br />
		<select id="rightSelect" size="9" style="width: 500px">
		</select>
		<br />
		<input class="bottomButton" type="button" id="selectFileButton" value="Select file" />
	</div>
</div>
