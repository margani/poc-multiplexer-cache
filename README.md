# Multiplexer PoC

This is the PoC for using two redis instances with one ConnectionMultiplexer with comma separation of connection strings.

The result is that after running TestPutGet method, it writes only to the first connection string which is primaryConnectionString.
