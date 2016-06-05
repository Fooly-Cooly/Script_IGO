if(!$addedInGameMaps)
{
	$remapDivision[$remapCount] = "IGSO";
	$remapName[$remapCount] = "IGO Window";
	$remapCmd[$remapCount] = "IGO_Window";
	$remapCount++;
	$remapName[$remapCount] = "Admin Chat";
	$remapCmd[$remapCount] = "IGO_AdminChat";
	$remapCount++;
	$IGSOTotalColors = 0;
}

function IGO_Window(%val)
{
	if(!%val) return;
	switch(IGO_Window.isAwake())
	{
		case 0:	IGO_RequestList(1);
				IGO_RequestList(2);
				IGO_RequestList(3);
				commandToServer("IGO_Update");
				canvas.pushdialog(IGO_GUI);
		case 1:	canvas.popDialog(IGO_GUI);
	}
}

function IGO_Tab(%ui)
{
	%i = 0;
	while((%obj = IGO_Window.getObject(%i)) != -1 && (%i++))
	{
		switch$(%obj.getClassName())
		{
			case "GuiSwatchCtrl":		%obj.setVisible(0);
			case "GuiBitmapButtonCtrl":	%obj.setColor("100 100 100 255");
		}
	}
	%tab = %ui@"_TAB";
	%tab.setVisible(1);
	%btn = %ui@"_BTN";
	%btn.setColor("0 0 0 0");
}

function IGO_SetPref()
{
	commandtoserver('IGO_SetPref',IGO_Prefs.getSelected(),IGO_PrefInput.getValue());
}

function IGO_Gui::PushGui(%this,%gui,%obj,%stg)
{
	switch(%stg)
	{
		case 1:	canvas.popdialog(%gui);
				canvas.pushdialog(%this);
				%gui.getobject(0).getObject(%obj).command = "canvas.popdialog(" @ %gui @ ");canvas.pushdialog(AdminGui);";
		default:canvas.popdialog(%this);
				canvas.pushdialog(%gui);
				%gui.getobject(0).getObject(%obj).command = "IGO_GUI.PushGui(" @ %gui @ "," @ %obj @ ",1);";
	}
}

function IGO_Prefs::OnSelect(%this)
{
	IGO_Input.setVisible(1);
}

function clientCmdIGO_Recieve(%type,%name,%tog,%txt)
{
	switch$(%type)
	{
		case "Text":	%name.setValue(%tog);
		case "List":	%name.addRow(%tog,%txt);
		case "Button":	%name.mColor = %tog == 0 ? $IGSOC::OffColor : $IGSOC::OnColor;
	}
}