function IGO_Clean(%cl, %msg)
{
	%len = strlen(%msg);
	%msg = %len > $Pref::Server::MaxChatLen ? getSubStr(%msg, 0, $Pref::Server::MaxChatLen) : %msg;
	%msg = $Pref::server::eTardFilter && !chatFilter(%cl, %msg, $Pref::Server::ETardList, '\c5This is a civilized game.  Please use full words.') ? 0 : %msg;
	return %msg;
}