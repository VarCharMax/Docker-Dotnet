mv /etc/nginx/nginx.conf /etc/nginx/nginx.conf.orig
mv /tmp/nginx.conf /etc/nginx/nginx.conf
mv /tmp/index.html  /home/www/index.html
mkdir /run/openrc
openrc
touch /run/openrc/softlevel
echo 'rc_provide="loopback net"' >> /etc/rc.conf
# rc-update add nginx default
rc-service nginx start