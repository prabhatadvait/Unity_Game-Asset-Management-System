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

$username = $_POST["name"];
$password = $_POST["password"];

$namecheckquery = "SELECT username, salt, hash, score FROM players WHERE username='" . $username . "';";

$namecheck = $con->query($namecheckquery) or die("2: namecheckquery failed"); // error code #2 = namecheckquery failed

if ($namecheck->num_rows != 1) {
    echo "5: Either no user with name, or more than one"; // error code #5 = number of names matching != 1
    exit();
} 

// Get login info from query
$existinginfo = $namecheck->fetch_assoc();
$salt = $existinginfo["salt"];
$hash = $existinginfo["hash"];

$loginhash = crypt($password, $salt);

if ($hash != $loginhash) {
    echo "6: Incorrect password"; // error code #6 = password does not hash to match table
    exit();
}

// If everything is correct, output the expected response
echo "0\t" . $existinginfo["score"];
?>
