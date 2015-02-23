#!/bin/bash
echo Killing $(cat rollo.pid)
kill $(cat rollo.pid)
echo Removing rollo.pid
rm rollo.pid
echo Rollo Server stopped

