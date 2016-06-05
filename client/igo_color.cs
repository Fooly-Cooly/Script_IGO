function IGO_ButtonColor(%Tog)
{
	%Selected = IGO_ColorList.getSelectedId();
	if(%Selected !$= -1)
	{
		switch$(%Tog)
		{
			case "On":	$IGSOC::OnColor = $IGSOColor[%Selected];
			case "Off":	$IGSOC::OffColor = $IGSOColor[%Selected];
		}
		echo("Setting IGSO Button Colors...");
		IGO_setButtonColor2();
		commandToServer('UpdateIGSO');
		echo("Saving Button Color settings...");
		export("$IGSOC::*","Config/Client/IGSO/Prefs.cs", False);
	}
	else MessageBoxOK("IGSO Alert!","Please select a color.");
}

function IGSO_setButtonColor2()
{
	//--IGSO Window------------------------------------------
	btnWand.mColor = $IGSOC::OnColor;
	btnMap.mColor = $IGSOC::OnColor;
	btnClearBrick.mColor = $IGSOC::OffColor;
	btnClearChat.mColor = $IGSOC::OffColor;
	btnMaxP.mColor = $IGSOC::OnColor;
	btnMaxSub.mColor = $IGSOC::OffColor;
	btnAutoAdmin.mColor = $IGSOC::OnColor;
	btnSpy.mColor = $IGSOC::OnColor;
	btnUnBan.mColor = $IGSOC::OnColor;
	btnAuto.mColor = $IGSOC::OnColor;
	btnIGSOColors.mColor = $IGSOC::OnColor;
	btnEtardW.mColor = $IGSOC::OnColor;
	btnKick.mColor = $IGSOC::OffColor;
	btnBan.mColor = $IGSOC::OffColor;
	btnSortP.mColor = $IGSOC::OnColor;
	btnSortB.mColor = $IGSOC::OnColor;
	btnSortA.mColor = $IGSOC::OnColor;

	//--Colors Window----------------------------------------
	btnAddColor.mColor = $IGSOC::OnColor;
	btnSetOn.mColor = $IGSOC::OnColor;
	btnSetOff.mColor = $IGSOC::OffColor;
	btnRemoveColor.mColor = $IGSOC::OffColor;

	//--Admin Window-----------------------------------------
	btnAddSuperAdmin.mColor = $IGSOC::OnColor;
	btnAddAdmin.mColor = $IGSOC::OnColor;
	btnRemoveSuperAdmin.mColor = $IGSOC::OffColor;
	btnRemoveAdmin.mColor = $IGSOC::OffColor;
	btnUnAutoAdmin.mColor = $IGSOC::OffColor;
	btnUnAutoSuper.mColor = $IGSOC::OffColor;
	btnAutoAdmin.mColor = $IGSOC::OnColor;
	btnAutoSuper.mColor = $IGSOC::OnColor;

	//--Filters Window---------------------------------------
	btnAddFilter.mColor = $IGSOC::OnColor;
	btnRemoveFilter.mColor = $IGSOC::OffColor;
	btnClearFilter.mColor = $IGSOC::OffColor;
	btnResetFilter.mColor = $IGSOC::OffColor;

	//--Pm Window--------------------------------------------
	btnPmSend.mColor = $IGSOC::OnColor;
	btnClearPm.mColor = $IGSOC::OffColor;
}

function IGO_getColorList()
{
	for(%i=0; %i < $IGSOTotalColors; %i++)
		IGO_ColorList.addColor(%i,$IGSOColor[%i]);
 }

function IGO_RenderColor(%i)
{
	%ui = "IGO_Slider"@%i;
	%value = %ui.getValue() * 255;
	%end = strPos(%value,".");
	%value = %end > 0 ? getSubStr(%value,0,%end) : %value;
	%color = IGO_Result.Color;
	%color = setWord(%color,%i,%value);
	IGO_Result.Color = %color;
}

function IGO_ColorList::addColor(%This,%Num,%Color)
{
		%Ext = (25*%Num);
		%Pos = "0"SPC %Ext;
		if(getWord(%This.extent,1) < %Ext){%This.extent = VectorAdd(%This.extent,"0 25");}
		%Ui = new GuiSwatchCtrl()
		{
			extent = "295 25";
			position = %Pos;
			color = %Color;
			new GuiBitmapButtonCtrl() {
				profile = "BlockButtonProfile";
				position = "0 0";
				extent = "25 25";
				text = "On";
				bitmap = "Add-Ons/Script_IGSO/UI/buttonTrans";
				mColor = "255 255 255 255";
			};
			new GuiBitmapButtonCtrl() {
				profile = "BlockButtonProfile";
				position = "133 0";
				extent = "25 25";
				text = "Off";
				bitmap = "Add-Ons/Script_IGSO/UI/buttonTrans";
				mColor = "255 255 255 255";
			};
			new GuiBitmapButtonCtrl() {
				profile = "BlockButtonProfile";
				position = "255 0";
				extent = "25 25";
				text = "X";
				bitmap = "Add-Ons/Script_IGSO/UI/buttonTrans";
				mColor = "255 255 255 255";
			};
		};
		%This.add(%Ui);
}

function IGO_EditColor(%Arg)
{
	switch$(%Arg)
	{
		case "Add":		%Color = IGSO_Result.color;
						$IGSOColor[$IGSOTotalColors] = %Color;
						IGSOColorList.addColor($IGSOTotalColors,%Color);
						$IGSOTotalColors++;
						//IGSO_WriteColor("Add",%Color);
		case "remove":	%Selected = IGSOColorList.getSelectedId();
						if(%Selected !$= -1)
							messageboxokcancel("IGSO Alert!","Are you sure you want to remove" SPC $IGSOColorName[%Selected] @ "?","IGSO_RemoveColor();","");
						else
							MessageBoxOK("IGSO Alert!","Please select a color.");
	}
}

function IGSO_WriteColor(%Cmd,%Name,%Color)
{
	%File = new FileObject();
	switch$(%Cmd)
	{
		case "Add":		%File.openForAppend("Config/Client/IGSO/Colors.cs");
						%Color = ("$IGSOColor[$IGSOTotalColors] =" SPC "\"" @ %Color @ "\"" @ ";");
						%File.writeLine(%Color);
						%File.writeLine("$IGSOTotalColors++;");
						%File.writeLine(" ");
		case "Remove":	%File.openForWrite("Config/Client/IGSO/Colors.cs");
						%File.writeLine(" ");
	}
	%File.close();
	%File.delete();
}

function IGSO_RemoveColor()
{
	%Num = IGSOColorList.getSelectedID();
	for(%i=%Num;%i<$IGSOTotalColors;%i++)
	{
		$IGSOColorName[%i] = $IGSOColorName[%i+1];
		$IGSOColor[%i] = $IGSOColor[%i+1];
		$IGSOColorName[%i+1] = "";
		$IGSOColor[%i+1] = "";
	}
	$IGSOTotalColors--;
	IGSO_WriteColor("Remove");
	for(%i=0;%i<$IGSOTotalColors;%i++)
	{
		IGSO_WriteColor("Add",$IGSOColor[%i]);
	}
	IGSO_getColorList();
}