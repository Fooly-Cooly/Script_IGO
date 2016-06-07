function IGO_MessageAdmin(%lvl, %msg)
{
	%i = 0;
	while((%cl = ClientGroup.getObject(%i)) && (%i++))
		if((%cl.isSuperAdmin + %cl.isAdmin) == %lvl)
			messageClient(%cl, '', %msg);
}

function IGOCmd_Admin(%cl, %msg)
{
	%type = %cl.isLan() + %cl.isSuperAdmin + %cl.isAdmin;
	switch(%type)
	{
		case 1: %admin = "Admin";
		case 2: %admin = "Super Admin";
		case 3: %admin = "Host";
		default:return;
	}
	if((%msg = IGO_Clean(%cl, %msg)) !$= "")
	{
		%msg = "[\c3" @ %admin @ "\c0] \c7" @ %cl.ClanPrefix @ "\c3" @ %cl.name @ "\c7" @ %cl.ClanSuffix @ "\c6: \c0" @ %msg;
		IGO_MessageAdmin(1, %msg);
	}
}