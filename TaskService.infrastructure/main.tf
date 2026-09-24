terraform {
  required_providers {
    hcloud = {
      source  = "hetznercloud/hcloud"
      version = "1.69.0"
    }
  }
}

provider "hcloud" {
  token = var.hcloud_token
}

resource "hcloud_server" "server" {
    name = "Nginx"
    image = "ubuntu-24.04"
    server_type = "cpx12"
    location = "hel1"

    ssh_keys = [var.ssh_key_name]

    connection {
        type = "ssh"
        user = "root"
        private_key = file(pathexpand("~/.ssh/id_ed25519"))
        host = self.ipv4_address
    }

    provisioner "remote-exec" {
        inline = [
            "apt update",
            "apt install docker.io -y",
            "apt install docker-compose -y",
            "docker run -d -p 80:80 nginx"
        ]
    }
}

output "server_ip" {
    value = hcloud_server.server.ipv4_address
}
