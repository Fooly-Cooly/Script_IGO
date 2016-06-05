exec("./server/igo_admin.cs");
exec("./server/igo_lists.cs");
exec("./server/igo_messages.cs");
exec("./server/igo_chathook.cs");

function serverCmdIGO_Gateway(%cl, %cmd, %arg0, %arg1, %arg2)
{
	if($Sim::Time < %cl.lastIGO + 1) return;
	%cl.lastIGO = $Sim::Time;
	call("IGO_" @ %cmd, %cl, %arg0, %arg1, %arg2);
}

function IGO_SetAdmin(%cl, %id, %type)
{
	if(!%cl.isLan() || %id.isLan() || !isObject(%id) || ((%id.isSuperAdmin + %id.isAdmin) == %type))
		return;
	switch$(%type)
	{
		case 0:	%msg = "has been UnAdmined.";
				%id.isAdmin = 0;
				%id.isSuperAdmin = 0;
		case 1:	%msg = %id.isSuperAdmin ? "has been demoted to Admin (Host)" : "has become Admin (Host)";
				%id.isAdmin = 1;
				%id.isSuperAdmin = 0;
		case 2:	%msg = "has become Super Admin (Host)";
				%id.isAdmin = 1;
				%id.isSuperAdmin = 1;
	}
	commandtoClient(%id, 'setAdminLevel', %type);
	%id.updatePlayerList();
}