IGO_RegisterServerChatHook("IGO_FloodProtection", 2);

function IGO_FloodProtection(%cl,%msg)
{
	error("called");
	%msg	= trim(%msg);
	%lstMsg = %cl.lastMsg;
	%length = strlen(%msg);
	%lstLen = strlen(%lstMsg);
	
	error("first", %msg);
	error("last", %lstMsg);
	
	%tokens	= "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,!,@,#,$,%,^,&,*,(,),-,_,=,+,:,/, ";
	while((%tokens = nextToken(%tokens, "char", ",")) !$= "")
	{
		%nMsg			= stripChars(%lstMsg, %char);

		%lstCnt[%char]	= %lstLen - strlen(%nMsg);
		%nMsg			= stripChars(%msg, %char);

		%cnt[%char]		= %length - strlen(%nMsg);
	}
		
	%tokens	= "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,!,@,#,$,%,^,&,*,(,),-,_,=,+, ";
	while((%tokens = nextToken(%tokens, "char", ",")) !$= "")
	{
		//echo("Last" SPC %char SPC "count:" SPC %lstCnt[%char]);
		//echo("Curr" SPC %char SPC "count:" SPC %cnt[%char]);
		%dif = MAbs(%lstCnt[%char] - %cnt[%char]);
		%chk = %dif > 5 ? %chk++ : %chk;
	}

	%cl.lastMsg = %msg;
	
	if(%chk > 3 && %cl.lastMsgTime > $Sim::Time)
		return;
	
	%cl.lastMsgTime = $Sim::Time + 3;
	return %msg;
	
	//%msg = trim(%msg);
	//%cnt = strlen(%msg);
	//%dif = strlen(%cl.lastMsg);
	//%dif = MAbs(%cnt - %dif);
	//for(%i=0; %i < %cnt; %i++)
	//{
	//	%char0 = getSubStr(%msg, %i, 1);
	//	%char1 = getSubStr(%cl.lastMsg, %i+%dif, 1);
	//	if(%char0 $= %char1) %chk++;
	//}
	//%chk = (%chk * 100) / %cnt;
	//%cl.lastMsg = %msg;
	//if(%chk > 60 && %cl.lastMsgTime > $Sim::Time)
	//	return;
	//%cl.lastMsgTime = $Sim::Time + 3;
	//return %msg;
}