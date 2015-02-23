#!/bin/bash
echo Starting Rollo server...
./RolloServerLinux64.x86_64 -batchmode -nographics -logfile log >> stdlog.txt 2>&1 &
echo Writing pid $! to rollo.pid
echo $! > rollo.pid
echo Rollo Server started!

