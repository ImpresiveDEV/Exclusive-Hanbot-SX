<?php

$dbhost   = "<server>";
$dbname   = "<name_db>";
$dbuser   = "<login>";
$dbpass   = "<password>";
$dbtable  = "<table>";

// Connect to Database
$mysqli = new mysqli($dbhost, $dbuser, $dbpass) or die("Code: #");
$verb = $mysqli->select_db($dbname);

?>