function IGO_RequestList(%cl,%list)
{
	%i = 0;
	switch(%list)
	{
		//1 = Auto Admin | 2 = eTard | 3 = Player
		case 1:	while((%id = getWord($Pref::Server::AutoAdminList,%i)) !$= "" && (%i++))
					commandToClient(%cl,'IGO_Recieve',"List","IGO_AutoAdmin_LST",%id,%id);
				%i = 0;
				while((%id = getWord($Pref::Server::AutoSuperAdminList,%i)) !$= "" && (%i++))
					commandToClient(%cl,'IGO_Recieve',"List","IGO_AutoSuper_LST",%id,%id);
		case 2:	%tkns = $Pref::Server::ETardList;
				while((%tkns = nextToken(%tkns,"word",",")) !$= "" && (%i++))
					commandToClient(%cl,'IGO_Recieve',"List","IGO_eTard_LST",%i,%word);
		case 3:	while((%obj = ClientGroup.getObject(%i)) != -1 && (%i++))
				{
					%chk = %obj.isAdmin + %obj.isSuperAdmin + %obj.isLan();
					switch(%chk)
					{
						case 1: %type = "A";
						case 2: %type = "S";
						case 3: %type = "H";
						default:%type = "";
					}
					CommandToClient(%cl,'IGO_Recieve',"List","IGO_Player_LST",%obj,%type TAB %obj.name TAB %obj.bl_id TAB %obj.getRawIp());
				}
	}
}

function IGO_AutoEdit(%cl,%act,%blid)
{
	if(!%cl.isSuperAdmin) return;
	%i = 0;
	switch(%act)
	{
		//1 = Super | 2 = Admin | 3 = UnSuper | 4 = UnAdmin
		case 1:	%list = $Pref::Server::AutoSuperAdminList;
				while((%id = getWord(%list,%i)) !$= "" && (%i++)) if(%id $= %blid) return;
				$Pref::Server::AutoSuperAdminList = %list $= "" ? %blid : %list SPC %blid;
		case 2:	%list = $Pref::Server::AutoAdminList;
				while((%id = getWord(%list,%i)) !$= "" && (%i++)) if(%id $= %blid) return;
				$Pref::Server::AutoAdminList = %list $= "" ? %blid : %list SPC %blid;
		case 3:	%list = $Pref::Server::AutoSuperAdminList;
				$Pref::Server::AutoSuperAdminList = removeWord(%list,%id);
		case 4:	%list = $Pref::Server::AutoAdminList;
				$Pref::Server::AutoAdminList = removeWord(%list,%id);
	}
	IGO_SavePrefs();
}

function IGO_eTardEdit(%cl,%act,%word)
{
	if(!%cl.isSuperAdmin) return;
	switch(%act)
	{
		//1 = Add | 2 = Remove | 3 = Reset | 4 = Clear
		case 1:	$Pref::Server::ETardList = $Pref::Server::ETardList@%word;
		case 2:	$Pref::Server::ETardList = strReplace($Pref::Server::ETardList,%word,"");
		case 3:	$Pref::Server::ETardList = " u , r , ur , wat , wut , wuts , wit , dat , loel , y ,";
		case 4:	$Pref::Server::ETardList = "";
	}
	IGO_SavePrefs();
}