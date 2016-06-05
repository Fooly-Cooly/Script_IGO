function IGO_Player_LST::Sort(%this,%col)
{
	switch(%this.SortTog)
	{
		case 0:	%this.SortTog = 1;
		case 1:	%this.SortTog = 0;
	}
	switch$(%col)
	{
		case "Blid":	%this.sortnumerical(2,%this.SortTog);
		case "Name":	%this.sort(1,%this.SortTog);
		case "Status":	%this.sort(0,%this.SortTog);
	}
}

function IGO_Player_LST::Action(%this,%act,%admin)
{
	%id = %this.getSelectedId();
	if(!%id) return;
	%txt = %this.getRowTextById(%id);
	%type = getField(%txt,0);
	%name = getField(%txt,1);
	switch$(%act)
	{
		case "Admin":	commandToServer('IGO_SetAdmin',%id,%admin);
		case "Kick":	MessageBoxYesNo("Kick Player?","Are you sure you want to kick \""@%name@"\" ?","commandToServer('Kick',"@%id@");","");
		case "Ban":		addBan_Window.text = "Ban" @ %name;
						addBanGui.victimBL_ID = getField(%txt,2);
						addBanGui.victimId = %id;
						addBanGui.victimName = %name;
						canvas.pushdialog(addBanGui);
		case "Spy":		Canvas.popdialog(IGO_Window);
						commandToServer('spy',%Name);
	}
	IGO_RequestList(3);
}

function IGO_RequestList(%list)
{
	// 1 Admin | 2 eTard | 3 Player
	switch(%list)
	{
		case "1":	IGO_AutoAdmin_LST.Clear();
					IGO_AutoSuper_LST.Clear();
					commandToServer('IGO_RequestList', 1);
		case "2":	IGO_eTard_LST.Clear();
					commandToServer('IGO_RequestList', 2);
		case "3":	IGO_Player_LST.Clear();
					commandToServer('IGO_RequestList', 3);
	}
}

function IGO_AutoEdit(%act)
{
	switch$(%act)
	{
		case 1:	%id = IGO_AutoAdminAdd_TXT.getValue();
		case 2:	%id = IGO_AutoAdminAdd_TXT.getValue();
		case 3:	%id = IGO_AutoSuper_LST.getSelectedId();
				%id = IGO_AutoSuper_LST.getRowTextById(%id);
		case 4:	%id = IGO_AutoAdmin_LST.getSelectedId();
				%id = IGO_AutoAdmin_LST.getRowTextById(%id);
	}
	if(%id)
	{
		IGO_AutoAdminAdd_TXT.setValue("");
		commandToServer('IGO_AutoEdit',%act,%id);
	}
	else MessageBoxOK("IGO Alert!","Please type in a BLID first.");
}


function IGO_eTardEdit(%List,%Type)
{
	switch$(%Type)
	{
		case "Add":		%Word = txtFilterAdd.getValue();
						%Word = %Word@",";
						CommandToServer('IGSO_eTardEdit',%Type,%Word);
		case "Remove":	%ID = IGSO_eTardList.getSelectedID();
						%Word = IGSO_eTardList.getRowText(%ID)@",";
						CommandToServer('IGO_eTardEdit',%Type,%Word);
		case "Reset":	CommandToServer('IGO_eTardEdit',%Type);
		case "Clear":	CommandToServer('IGO_eTardEdit',4);
	}
	IGSO_UpdateLists(%List);
}