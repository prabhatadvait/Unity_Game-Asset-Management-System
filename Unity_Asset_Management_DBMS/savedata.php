<?php

$servername = "localhost";
$username = "root";
$password = "prabhat"; 
$dbname = "unityaccess";

// Create connection
$con = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($con->connect_error) {
    echo "1: Connection failed"; // error code #1 = connection failed
    exit();
}

if (!isset($_POST["name"]) || !isset($_POST["score"])) {
    echo "6: Missing POST variables"; // error code #6 = missing POST variables
    exit();
}

$username = $_POST["name"];
$newscore = $_POST["score"];

$namecheckquery = "SELECT username FROM players WHERE username='" . $username . "';";

$namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed");

if ($namecheck->num_rows != 1) {
    echo "5: Either no user with name, or more than one"; // error code #5 = number of names matching != 1
    exit();
}

// Add a space before the WHERE clause
$updatequery = "UPDATE players SET score = " . $newscore . " WHERE username = '" . $username . "';";
mysqli_query($con, $updatequery) or die("7: Save query failed"); // error 7: update failed

echo "0";

?>
