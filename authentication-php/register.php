<?php
if (isset($_POST['username']) && isset($_POST['password'])) 
{
	if(!@include("settings.php")){
		die("Code: #");
	}
	
	$username		= mysqli_real_escape_string($mysqli,$_POST['username']);
	$ppasword		= mysqli_real_escape_string($mysqli,$_POST['password']);
	$active			= "false";
	$try			= "0";
	
	$check_username 	= "SELECT * FROM ".$dbtable." WHERE username='".$username."'";
	$result_username 	= $mysqli->query($check_username);
	
	if (mysqli_num_rows($result_username) > 0)  {
		die("Code: #");
	}
	
	$sql = "INSERT INTO `TestDB`.`Users` (`username`, `password`,`try`, `active`) VALUES ('".$username."', '".$password."', '".$try."', '".$active."')";
	$result = $mysqli->query($sql);

	if (!$result) {
		die("Code: #");
	} else {
		die("Code: #");
	}

	$mysqli->Close();
}
?>
