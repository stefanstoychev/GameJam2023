FROM nginx:1.21.5-alpine

COPY default.conf /etc/nginx/conf.d/default.conf

COPY ./Build/webxr /usr/share/nginx/html
